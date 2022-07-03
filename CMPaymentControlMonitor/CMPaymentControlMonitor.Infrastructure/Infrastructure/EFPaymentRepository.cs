using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.WebInterface;

namespace CMPaymentControlMonitor.Infrastructure.Infrastructure
{
    public class EFPaymentRepository : IPaymentRepository
    {
        private paymentsolaptestforstudentsContext context;

        public EFPaymentRepository(paymentsolaptestforstudentsContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Payment> Payments =>
            context.Payments.AsQueryable();
    }
}
