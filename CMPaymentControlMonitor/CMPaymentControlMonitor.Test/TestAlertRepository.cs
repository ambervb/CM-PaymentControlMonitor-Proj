using CMPaymentControlMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMPaymentControlMonitor.Test
{
    public class TestAlertRepository : IAlertRepository
    {
        public TestAlertRepository() =>
            Alerts = new List<Alert>();

        public ICollection<Alert> Alerts { get; set; }
    

        public IControlCheckRepository Object { get; internal set; }

        public void ResolveAlert(Alert alert)
        {
            var dbAlert = Alerts.FirstOrDefault(a =>
                a.Id == alert.Id);

            dbAlert.Comment = alert.Comment;
            dbAlert.Resolved = true;
        }
    }
}
