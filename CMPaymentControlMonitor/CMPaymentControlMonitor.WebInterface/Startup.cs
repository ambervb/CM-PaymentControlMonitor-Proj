using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.Infrastructure.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMPaymentControlMonitor.WebInterface
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<paymentsolaptestforstudentsContext>(options =>
                options.UseSqlServer(Configuration["LocalDbData:CMTransactions:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration["LocalDbData:AppIdentity:ConnectionString"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.ConfigureApplicationCookie(options =>
                options.LoginPath = "/Account/Login");

            services.AddTransient<IControlCheckRepository, EFControlCheckRepository>();
            services.AddTransient<IAlertRepository, EFAlertRepository>();

            //services.AddSession();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseSession();
            app.UseAuthentication();

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Dashboard}/{id?}");
            });

            IdentitySeedData.SetupTestAccount(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>());
            CMAppSeedData.SetupTestPaymentData(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>());
        }
    }
}
