using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.Infrastructure.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Formatters;
using Halcyon.Web.HAL.Json;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using CMPaymentControlMonitor.WebInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMPaymentControlMonitor.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;


        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services
                .AddMvc()
                .AddMvcOptions(c => {
                    var jsonOutputFormatter = new JsonSerializerSettings();
                    c.OutputFormatters.Add(new JsonHalOutputFormatter(
                        jsonOutputFormatter,
                        halJsonMediaTypes: new string[] { "application/hal+json", "application/vnd.example.hal+json", "application/vnd.example.hal.v1+json" }
                    ));
                });

            services.AddDbContext<paymentsolaptestforstudentsContext>(options =>
                options.UseSqlServer(Configuration["LocalDbData:CMTransactions:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration["LocalDbData:AppIdentity:ConnectionString"]));

            services.AddTransient<IControlCheckRepository, EFControlCheckRepository>();
            services.AddTransient<IUserRepository, EFUserRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            //swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SOA API ",
                    Description = "Documentation for SOA API, group A4"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                //adds swagger authentication
                //use "Bearer " before entering token in swagger
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Authorization", new string[] { }},
                };


                c.AddSecurityDefinition("Authorization", new ApiKeyScheme
                {
                    Type = "apiKey",
                    Name = "Authorization",
                    In = "header",

                    
                });
                c.AddSecurityRequirement(security);



            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            //Swagger config
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });


            app.UseMvc();

        }
    }
}
