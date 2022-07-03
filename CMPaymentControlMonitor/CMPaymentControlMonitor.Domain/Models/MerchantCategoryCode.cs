using System;
using System.Collections.Generic;

namespace CMPaymentControlMonitor.Domain.Models
{
    public partial class MerchantCategoryCode
    {
        public MerchantCategoryCode()
        {
            Merchants = new HashSet<Merchant>();
        }

        public int Mcc { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Merchant> Merchants { get; set; }
    }
}
