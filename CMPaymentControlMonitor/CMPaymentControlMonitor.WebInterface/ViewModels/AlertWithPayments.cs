using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMPaymentControlMonitor.Domain.Models;

namespace CMPaymentControlMonitor.WebInterface.ViewModels
{
    public class AlertWithPayments
    {
        public Alert Alert { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
