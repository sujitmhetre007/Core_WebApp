using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_WebApp.Models;
using Core_WebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core_WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // Registers objects in dependancy
        //1. Database Context
        //    - EF Core DbContext
        // 2. MVC Options
        //    - Filters
        //    - Formatters
        // 3. Security
        //    Authentication for Users
        //    Authrizations
        //     - Based On Roles
        //        - Role based Ploicies
        //    Based on JSON Web Token
        // 4. Cookies
        // 5. CORS Policies
        //     - web APIS
        // 6. Custom Services
        //     - Domain Based Services class aka businesss logic
        //7. Sessions 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //Register the DbContext class in DI
            services.AddDbContext<CoreAppDBContext>((options) =>
            {
                //use sql sever defined using the connection string
                options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection"));                
            });

            //register repository classes in DI Container
            services.AddScoped<IRepository<Category, int>, CategoryRepository>();
            services.AddScoped<IRepository<Product, int>, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        /*
         IApplicationBuilder  --> Used to manage HttpRequest using 'Middlewares'
         IWebHostEnvironment --> Detect the Hosting env. for execution
         dotnet tool uninstall --global dotnet-ef
         dotnet tool install --global dotnet-ef --version 3.1.0
         dotnet ef migrations add firstMigration -c Core_WebApp.Models.AddDbContext
         dotnet ef database update -c Core_WebApp.Models.AddDbContext

            read repository pattern
     */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) // Devlopment mode
            {
                app.UseDeveloperExceptionPage();
            }
            else //Deployable environment
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //use .js/css/img files from wwwroot folder
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //Server endpoint accept request and start routing asp.Net core 3.0
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
