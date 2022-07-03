using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.WebInterface;
using Microsoft.EntityFrameworkCore;

namespace CMPaymentControlMonitor.Infrastructure.Infrastructure
{
    public class EFAlertRepository : IAlertRepository
    {
        private paymentsolaptestforstudentsContext context;

        public EFAlertRepository(paymentsolaptestforstudentsContext ctx)
        {
            context = ctx;
            
        }

        public ICollection<Alert> Alerts =>
            context.Alerts.ToList();

        public void ResolveAlert(Alert alert)
        {
            context.Update(alert);
            context.SaveChanges();
        }
    }
}
