using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMPaymentControlMonitor.Domain.Models
{
    public partial class Payment
    {
       
        public Payment()
        {
            Alerts = new HashSet<Alert>();
        }
        public long? Id { get; set; }
        public decimal MerchantAmount { get; set; }
        public string CreditCardBin { get; set; }
        public string Status { get; set; }
        public string StatusDetails { get; set; }
        public DateTime? PaymentCreatedOn { get; set; }

        public virtual Order Order { get; set; }
        [ForeignKey("PaymentMethod")]
        public string PaymentMethodNavigationId { get; set; }

        
        public virtual PaymentMethod PaymentMethodNavigation { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}
