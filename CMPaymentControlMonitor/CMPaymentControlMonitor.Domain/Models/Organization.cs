using System;
using System.Collections.Generic;

namespace CMPaymentControlMonitor.Domain.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Merchants = new HashSet<Merchant>();
        }

        public string Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string AccountManagerName { get; set; }

        public virtual ICollection<Merchant> Merchants { get; set; }
    }
}
