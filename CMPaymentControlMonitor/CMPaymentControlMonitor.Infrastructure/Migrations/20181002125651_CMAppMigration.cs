using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMPaymentControlMonitor.Domain.Migrations
{
    public partial class CMAppMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControlChecks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlChecks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    IsoCode = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.IsoCode);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyCode = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Digits = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    ExchangeRateToEuro = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyCode);
                });

            migrationBuilder.CreateTable(
                name: "MerchantCategoryCodes",
                columns: table => new
                {
                    Mcc = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantCategoryCodes", x => x.Mcc);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    ID = table.Column<string>(unicode: false, maxLength: 32, nullable: false),
                    Guid = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    AccountManagerName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethod = table.Column<string>(maxLength: 255, nullable: false),
                    IsPrepaid = table.Column<bool>(nullable: true),
                    IsCreditCard = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethod);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    ID = table.Column<string>(unicode: false, maxLength: 32, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Country = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    City = table.Column<string>(maxLength: 255, nullable: true),
                    MerchantCategoryCodeNavigationMcc = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Merchants_MerchantCategoryCodes",
                        column: x => x.MerchantCategoryCodeNavigationMcc,
                        principalTable: "MerchantCategoryCodes",
                        principalColumn: "Mcc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Merchants_Organizations",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false),
                    BuyerName = table.Column<string>(maxLength: 511, nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: true),
                    Currency = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
                    Reference = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    MerchantID = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    MerchantCreatedOrderOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    OrderCreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    BuyerBillingCountryNavigationIsoCode = table.Column<string>(nullable: true),
                    BuyerShippingCountryNavigationIsoCode = table.Column<string>(nullable: true),
                    CurrencyNavigationCurrencyCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Billing",
                        column: x => x.BuyerBillingCountryNavigationIsoCode,
                        principalTable: "Countries",
                        principalColumn: "IsoCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Shipping",
                        column: x => x.BuyerShippingCountryNavigationIsoCode,
                        principalTable: "Countries",
                        principalColumn: "IsoCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Currency",
                        column: x => x.CurrencyNavigationCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Merchant",
                        column: x => x.MerchantID,
                        principalTable: "Merchants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false),
                    MerchantAmount = table.Column<decimal>(type: "money", nullable: true),
                    CreditCardBIN = table.Column<string>(maxLength: 255, nullable: true),
                    Status = table.Column<string>(maxLength: 255, nullable: true),
                    StatusDetails = table.Column<string>(maxLength: 255, nullable: true),
                    PaymentCreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    OrderId = table.Column<long>(nullable: true),
                    PaymentMethodNavigationPaymentMethod = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payments_Order",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_PayMethod",
                        column: x => x.PaymentMethodNavigationPaymentMethod,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethod",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Resolved = table.Column<bool>(nullable: true),
                    Comment = table.Column<string>(maxLength: 255, nullable: true),
                    AlertCreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CheckNavId = table.Column<int>(nullable: true),
                    PaymentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Alerts_ControlCheck",
                        column: x => x.CheckNavId,
                        principalTable: "ControlChecks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alerts_Paymentsl",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_CheckNavId",
                table: "Alerts",
                column: "CheckNavId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_PaymentId",
                table: "Alerts",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_MerchantCategoryCodeNavigationMcc",
                table: "Merchants",
                column: "MerchantCategoryCodeNavigationMcc");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_OrganizationId",
                table: "Merchants",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerBillingCountryNavigationIsoCode",
                table: "Orders",
                column: "BuyerBillingCountryNavigationIsoCode");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerShippingCountryNavigationIsoCode",
                table: "Orders",
                column: "BuyerShippingCountryNavigationIsoCode");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CurrencyNavigationCurrencyCode",
                table: "Orders",
                column: "CurrencyNavigationCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MerchantID",
                table: "Orders",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodNavigationPaymentMethod",
                table: "Payments",
                column: "PaymentMethodNavigationPaymentMethod");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "ControlChecks");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "MerchantCategoryCodes");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
