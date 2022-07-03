using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace CMPaymentControlMonitor.Domain.Models
{
    public interface IUserRepository
    {
        IdentityUser GetUser(string username);
    }
}
