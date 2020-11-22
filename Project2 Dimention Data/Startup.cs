using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project2_Dimention_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Project2_Dimention_Data.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using ServiceStack.Auth;

namespace Project2_Dimention_Data
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllersWithViews();
            services.Configure<AuthOptions>(Configuration);

            var connectionString = Configuration.GetConnectionString("Database"); // Gets the connection string 

            services.AddDbContext<emp_infoContext>(options => options.UseSqlServer(connectionString)); //Gets the connection string
            services.AddScoped<Authenticate>();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Loads once 
            services.AddTransient<Cryptography>(); // Loads multiple times

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "UserManagement",
                    pattern: "Management/User/{action=Index}/{id?}");
                    
        });


        }
    }
}

