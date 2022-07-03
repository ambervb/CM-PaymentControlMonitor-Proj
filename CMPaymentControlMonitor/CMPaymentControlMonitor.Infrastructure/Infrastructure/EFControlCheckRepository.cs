using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.WebInterface;

namespace CMPaymentControlMonitor.Infrastructure.Infrastructure
{
    public class EFControlCheckRepository : IControlCheckRepository
    {
        private paymentsolaptestforstudentsContext context;

        public EFControlCheckRepository(paymentsolaptestforstudentsContext ctx)
        {
            context = ctx;
        }

        public ICollection<ControlCheck> ControlChecks =>
            context.ControlChecks.ToList();

        public void Add(ControlCheck check)
        {
            context.ControlChecks.Add(check);
            context.SaveChanges();
        }

        public void Edit(int id, ControlCheck cc)
        {
            var check = context.ControlChecks.FirstOrDefault(c => c.Id == id);

            check.Name = cc.Name;
            check.Description = cc.Description;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.ControlChecks.Remove(
                context.ControlChecks.FirstOrDefault(cc => cc.Id == id));

            context.SaveChanges();
        }
    }
}
