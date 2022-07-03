using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMPaymentControlMonitor.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CMPaymentControlMonitor.Infrastructure.Infrastructure
{
    public class EFUserRepository : IUserRepository
    {
        private AppIdentityDbContext Context { get; set; }

        public EFUserRepository(AppIdentityDbContext context)
        {
            Context = context;
        }

        public IdentityUser GetUser(string username)
        {
            return Context.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}
