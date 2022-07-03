using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.WebInterface;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CMPaymentControlMonitor.Infrastructure.Infrastructure
{
    public static class CMAppSeedData
    {
        public static async void SetupTestPaymentData(IServiceScopeFactory scopeFactory)
        {
            IServiceScope scope = scopeFactory.CreateScope();

            paymentsolaptestforstudentsContext context = scope.ServiceProvider
                .GetRequiredService<paymentsolaptestforstudentsContext>();

            context.Database.Migrate();

            if (!context.ControlChecks.Any())
            {
                var cc1 = new ControlCheck()
                {
                    Id = 1,
                    Name = "Selling drugs to US",
                    Description = "Drugs being sold to buyers in the United States of America",
                };

                var cc2 = new ControlCheck()
                {
                    Id = 2,
                    Name = "Selling to FATF countries",
                    Description = "Country of the buyer is on the list of the FATF list of high risk countries"
                };

                var cc3 = new ControlCheck()
                {
                    Id = 3,
                    Name = "Amount in 72h higher than 15k",
                    Description = "Amount in 72 hours is higher than 15.000 euros for a single buyer"
                };

                var cc4 = new ControlCheck()
                {
                    Id = 4,
                    Name = "Gambling to certain countries",
                    Description = "Payment related to gambling, where the buyer is from one of the following countries; DE, PO, JP, US, SG, DK, CY"
                };

                var cc5 = new ControlCheck()
                {
                    Id = 5,
                    Name = "Amount in 72h higher than 100k",
                    Description = "Amount in 72 hours is higher than 100.000 euros for a single merchant"
                };


                cc1.Alerts = new List<Alert>()
                {
                    new Alert()
                    {
                        AlertCreatedOn = DateTime.Now.Subtract(new TimeSpan(4, 0, 0, 0)),
                        Check = cc1,
                        Id = 1,
                        PaymentId = null,
                        Resolved = false,
                        Comment = null
                    },
                    new Alert()
                    {
                        AlertCreatedOn = DateTime.Now.Subtract(new TimeSpan(3, 0, 0, 0)),
                        Check = cc1,
                        Id = 2,
                        PaymentId = null,
                        Resolved = false,
                        Comment = null
                    }
                };

                cc2.Alerts = new List<Alert>()
                {
                    new Alert()
                    {
                        AlertCreatedOn = DateTime.Now.Subtract(new TimeSpan(6, 0, 0, 0)),
                        Check = cc1,
                        Id = 3,
                        PaymentId = null,
                        Resolved = false,
                        Comment = null
                    },
                    new Alert()
                    {
                        AlertCreatedOn = DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0)),
                        Check = cc1,
                        Id = 4,
                        PaymentId = null,
                        Resolved = false,
                        Comment = null
                    }
                };
                
                cc3.Alerts = new List<Alert>()
                {
                    new Alert()
                    {
                        AlertCreatedOn = DateTime.Now.Subtract(new TimeSpan(8, 0, 0, 0)),
                        Check = cc1,
                        Id = 5,
                        PaymentId = null,
                        Resolved = false,
                        Comment = null
                    },
                    new Alert()
                    {
                        AlertCreatedOn = DateTime.Now.Subtract(new TimeSpan(3, 0, 0, 0)),
                        Check = cc1,
                        Id = 6,
                        PaymentId = null,
                        Resolved = false,
                        Comment = null
                    }
                };

                cc4.Alerts = new List<Alert>()
                {
                    new Alert()
                    {
                        AlertCreatedOn = DateTime.Now.Subtract(new TimeSpan(6, 0, 0, 0)),
                        Check = cc1,
                        Id = 7,
                        PaymentId = null,
                        Resolved = false,
                        Comment = null
                    },
                    new Alert()
                    {
                        AlertCreatedOn = DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0)),
                        Check = cc1,
                        Id = 8,
                        PaymentId = null,
                        Resolved = false,
                        Comment = null
                    }
                };

                cc5.Alerts = new List<Alert>()
                {
                    new Alert()
                    {
                        AlertCreatedOn = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)),
                        Check = cc1,
                        Id = 9,
                        PaymentId = null,
                        Resolved = false,
                        Comment = null
                    },
                    new Alert()
                    {
                        AlertCreatedOn = DateTime.Now,
                        Check = cc1,
                        Id = 10,
                        PaymentId = null,
                        Resolved = false,
                        Comment = null
                    }
                };

                context.ControlChecks.AddRange(
                    cc1, cc2, cc3, cc4, cc5);

                context.SaveChanges();
            }

        }
    }
}
