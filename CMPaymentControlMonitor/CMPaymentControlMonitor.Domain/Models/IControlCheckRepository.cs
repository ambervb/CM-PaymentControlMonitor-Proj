using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPaymentControlMonitor.Domain.Models
{
    public interface IControlCheckRepository
    {
        ICollection<ControlCheck> ControlChecks { get; }

        void Add(ControlCheck check);
        void Edit(int id, ControlCheck check);
        void Delete(int id);
    }
}
