using System;
using System.Collections.Generic;
using System.Text;

namespace CMPaymentControlMonitor.Domain.Models
{
    public interface IAlertRepository
    {
        ICollection<Alert> Alerts { get;  }
        void ResolveAlert(Alert alert);
    }
}
