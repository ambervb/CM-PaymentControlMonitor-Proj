using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMPaymentControlMonitor.Domain.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Payments = new HashSet<Payment>();
        }

        public string PaymentMethodUsed { get; set; }
        public bool IsPrepaid { get; set; }
        public bool IsCreditCard { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
