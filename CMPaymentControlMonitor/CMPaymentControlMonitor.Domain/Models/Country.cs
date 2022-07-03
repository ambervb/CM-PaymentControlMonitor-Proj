using System;
using System.Collections.Generic;

namespace CMPaymentControlMonitor.Domain.Models
{
    public partial class Country
    {
        public Country()
        {
            OrdersBuyerBillingCountryNavigation = new HashSet<Order>();
            OrdersBuyerShippingCountryNavigation = new HashSet<Order>();
        }

        public string IsoCode { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> OrdersBuyerBillingCountryNavigation { get; set; }
        public virtual ICollection<Order> OrdersBuyerShippingCountryNavigation { get; set; }
    }
}
