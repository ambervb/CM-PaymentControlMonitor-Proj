using System;
using System.Collections.Generic;

namespace CMPaymentControlMonitor.Domain.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Orders = new HashSet<Order>();
        }

        public string CurrencyCode { get; set; }
        public string Description { get; set; }
        public int Digits { get; set; }
        public bool IsActive { get; set; }
        public double? ExchangeRateToEuro { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
