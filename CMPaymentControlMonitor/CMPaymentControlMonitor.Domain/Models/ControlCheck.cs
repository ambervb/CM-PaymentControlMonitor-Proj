using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMPaymentControlMonitor.Domain.Models

{
  
    public partial class ControlCheck
    {
        public ControlCheck()
        {
            Alerts = new HashSet<Alert>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Alert> Alerts { get; set; }



    }
}
