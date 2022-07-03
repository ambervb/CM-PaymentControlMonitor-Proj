using System;
using System.Collections.Generic;

namespace CMPaymentControlMonitor.Domain.Models
{
    public partial class Order
    {
        public Order()
        {
            Payments = new HashSet<Payment>();
        }

        public long Id { get; set; }
        public string BuyerName { get; set; }
        public string BuyerBillingCountry { get; set; }
        public string BuyerShippingCountry { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string MerchantId { get; set; }
        public DateTime? MerchantCreatedOrderOn { get; set; }
        public DateTime? OrderCreatedOn { get; set; }

        public virtual Country BuyerBillingCountryNavigation { get; set; }
        public virtual Country BuyerShippingCountryNavigation { get; set; }
        public virtual Currency CurrencyNavigation { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
