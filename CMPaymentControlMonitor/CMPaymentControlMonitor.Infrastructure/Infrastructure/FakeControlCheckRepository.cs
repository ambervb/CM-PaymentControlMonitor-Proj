using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMPaymentControlMonitor.Domain.Models;

namespace CMPaymentControlMonitor.Infrastructure.Infrastructure
{
    public class FakeControlCheckRepository : IControlCheckRepository
    {
        public ICollection<ControlCheck> ControlChecks => new List<ControlCheck>
        {
            new ControlCheck()
            {
                Name = "Selling drugs to US",
                Id = 1,
                Description = "A control check that checks the payments.",
                Alerts = new List<Alert>
                {
                    new Alert()
                    {
                        Id = 1001,
                        Resolved = false,
                        Comment = "",
                        AlertCreatedOn = DateTime.Now,
                        Check = new ControlCheck()
	                    {
							Name = "Selling drugs to US",
							Id = 1,
							Description = "A control check that checks the payments."
						},
						PaymentId = new Payment()
						{
                            Id = 1001,
                            CreditCardBin = "CreditCardBin",
                            Status = "Status",
                            StatusDetails = "StatusDetails",
                            PaymentCreatedOn = new DateTime(2018, 6, 4),
                            MerchantAmount = 1,
                             PaymentMethodNavigation = new PaymentMethod()
                            {
                                PaymentMethodUsed = "creditcard",
                                IsCreditCard = true,
                                IsPrepaid = false,
                                
                            }
                        }
                    },
                    new Alert()
                    {
                        Id = 1002,
                        Resolved = false,
                        Comment = "",
                        AlertCreatedOn = DateTime.Now,
                        Check = new ControlCheck()
	                    {
		                    Name = "Selling drugs to US",
		                    Id = 1,
		                    Description = "A control check that checks the payments."
	                    },
						PaymentId = new Payment()
                        {
                            Id = 1001,
                            CreditCardBin = "CreditCardBin",
                            Status = "Status",
                            StatusDetails = "StatusDetails",
                            PaymentCreatedOn = new DateTime(2018, 6, 4),
                            MerchantAmount = 1,
                            PaymentMethodNavigation = new PaymentMethod()
                            {
                                PaymentMethodUsed = "creditcard",
                                IsCreditCard = true,
                                IsPrepaid = false
                                
                            }
                        }
                    }
                }
            },
            new ControlCheck()
            {
                Name = "High order value notification",
                Id = 2,
                Description = "A control check that checks the payments.",
                Alerts = new List<Alert>
                {
                }
            },
            new ControlCheck()
            {
                Name = "Financial Intelligence Unit (FIU) objective indicator 2",
                Id = 3,
                Description = "A control check that checks the payments.",
                Alerts = new List<Alert>
                {
                }
            },
            new ControlCheck()
            {
                Name = "Financial Intelligence Unit (FIU) objective indicator 6",
                Id = 4,
                Description = "A control check that checks the payments.",
                Alerts = new List<Alert>
                {
                }
            },
            new ControlCheck()
            {
                Name = "Block Gambling in certain countries",
                Id = 5,
                Description = "A control check that checks the payments.",
                Alerts = new List<Alert>
                {
                }
            }
        };

        public void Add(ControlCheck check)
        {
            ControlChecks.Add(check);
        }

        public void Delete(int id)
        {
            ControlChecks.Remove(
                ControlChecks.FirstOrDefault(cc => cc.Id == id));
        }

        public void Edit(int id, ControlCheck check)
        {
            var cc = ControlChecks.FirstOrDefault(c => c.Id == id);

            cc.Name = check.Name;
            cc.Description = check.Description;
        }
    }
}
