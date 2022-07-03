using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMPaymentControlMonitor.Infrastructure.Infrastructure
{
    public static class IdentitySeedData
    {
        private const string AccountName = "CMadmin";
        private const string AccountPassword = "!CadMin]A4";

        public static async void SetupTestAccount(IServiceScopeFactory scopeFactory)
        {
            IServiceScope scope = scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            var context = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
            context.Database.Migrate();

            IdentityUser user = await userManager.FindByIdAsync(AccountName);
            if(user == null && !userManager.Users.Any())
            {
                user = new IdentityUser()
                {
                    UserName = AccountName
                };

                await userManager.CreateAsync(user, AccountPassword);
            }

        }
    }
}
