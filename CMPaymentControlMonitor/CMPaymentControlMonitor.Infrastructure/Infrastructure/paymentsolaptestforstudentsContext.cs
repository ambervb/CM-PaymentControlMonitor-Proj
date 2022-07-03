using System;
using CMPaymentControlMonitor.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace CMPaymentControlMonitor.WebInterface
{
    public partial class paymentsolaptestforstudentsContext : DbContext
    {
       
        public paymentsolaptestforstudentsContext()
        {
           
        }

        public paymentsolaptestforstudentsContext(DbContextOptions<paymentsolaptestforstudentsContext> options)
            : base(options)
        {
            
        }
        
        public virtual DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<ControlCheck> ControlChecks { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<MerchantCategoryCode> MerchantCategoryCodes { get; set; }
        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            if (!optionsBuilder.IsConfigured)
            {
                
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source= tcp:cm-transaction.database.windows.net,1433;Initial Catalog=paymentsolaptestforstudents;Persist Security Info=False;User ID=CMAdmin;Password=!CadMin];MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");


               
                       

            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(e => e.Id );

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AlertCreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Resolved);

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.HasOne(d => d.Check)
                    .WithMany(p => p.Alerts);
                      

                entity.HasOne(d => d.PaymentId)
                    .WithMany(p => p.Alerts);
                  

            });
          
            modelBuilder.Entity<ControlCheck>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasMany(e => e.Alerts);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IsoCode);

                entity.Property(e => e.IsoCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.CurrencyCode);

                entity.Property(e => e.CurrencyCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MerchantCategoryCode>(entity =>
            {
                entity.HasKey(e => e.Mcc);

                entity.Property(e => e.Mcc).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);


                entity.Property(p => p.MerchantCode).HasColumnName("MerchantCategoryCode");



                entity.HasOne(d => d.MerchantCategoryCodeNavigation)
                    .WithMany(p => p.Merchants)
                    .HasForeignKey(e => e.MerchantCode);


                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Merchants)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Merchants_Organizations");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.BuyerBillingCountry)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.BuyerName).HasMaxLength(511);

                entity.Property(e => e.BuyerShippingCountry)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.MerchantCreatedOrderOn).HasColumnType("datetime");

                entity.Property(e => e.MerchantId)
                    .HasColumnName("MerchantID")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.OrderCreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Reference).HasMaxLength(255);

                entity.HasOne(d => d.BuyerBillingCountryNavigation)
                    .WithMany(p => p.OrdersBuyerBillingCountryNavigation)
                    .HasForeignKey(d => d.BuyerBillingCountry)
                    .HasConstraintName("FK_Orders_Countries");

                entity.HasOne(d => d.BuyerShippingCountryNavigation)
                    .WithMany(p => p.OrdersBuyerShippingCountryNavigation)
                    .HasForeignKey(d => d.BuyerShippingCountry)
                    .HasConstraintName("FK_Orders_Countries1");
              
                entity.HasOne(d => d.CurrencyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Currency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Currencies");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MerchantId)
                    .HasConstraintName("FK_Orders_Merchants");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AccountManagerName).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.PaymentMethodUsed);

                entity.Property(e => e.IsCreditCard);
                entity.Property(e => e.IsPrepaid);

                entity.HasMany(d => d.Payments).WithOne(p => p.PaymentMethodNavigation);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreditCardBin)
                    .HasColumnName("CreditCardBIN")
                    .HasMaxLength(255);

                entity.Property(e => e.MerchantAmount).HasColumnType("money");
                entity.Property(e => e.PaymentCreatedOn).HasColumnType("datetime");
                

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.Property(e => e.StatusDetails).HasMaxLength(255);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments);


                entity.Property(p => p.PaymentMethodNavigationId).HasColumnName("PaymentMethod");

                entity.HasOne(d => d.PaymentMethodNavigation)
                    .WithMany(p => p.Payments).HasForeignKey(e => e.PaymentMethodNavigationId);
              


            });
        }
    }
}
