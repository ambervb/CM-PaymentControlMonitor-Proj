
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMPaymentControlMonitor.Domain.Models
{
    public partial class Alert
    {
        public int Id { get; set; }
        public bool Resolved { get; set; }

        [StringLength(255, ErrorMessage = "The comment is too long")]
        public string Comment { get; set; }
        public DateTime AlertCreatedOn { get; set; }

        [ForeignKey("CheckID")]
        public virtual ControlCheck Check { get; set; }
        [ForeignKey("PaymentID")]
        public virtual Payment PaymentId { get; set; }
    }
}
