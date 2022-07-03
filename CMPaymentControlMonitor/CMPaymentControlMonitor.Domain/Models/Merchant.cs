using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMPaymentControlMonitor.Domain.Models
{
    public partial class Merchant
    {
        public Merchant()
        {
            Orders = new HashSet<Order>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        [ForeignKey("Mcc")]
        public int MerchantCode { get; set; }


       
        public virtual MerchantCategoryCode MerchantCategoryCodeNavigation { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
