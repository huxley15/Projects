using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//step 6.2
using WoodPlanning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
//step 28.6
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace WoodPlanning
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
            services.AddControllersWithViews();

            //step 6.2
            services.AddScoped<IClientRepository,DBClientRepository>();

            //step 29.6
            services.AddIdentity<Client, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;

            }).AddEntityFrameworkStores<ClientContext>();

            //step 9.2
            services.AddDbContext<ClientContext>(options => options.UseSqlServer("Server=LAPTOP-PPG329FS;Database= WPClient;Trusted_Connection = true;MultipleActiveResultSets=true"));

            

            //step 30.6
            //services.AddDbContext<ClientContext>(options => options.UseSqlServer("Server=LAPTOP-PPG329FS;Database= WPClient;Trusted_Connection = true;MultipleActiveResultSets=true"));
        }

        //step 37.6
        public async void CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] rolenames = { "Admin", "Client" };
            foreach (var rolename in rolenames)
            {
                bool roleExists = await roleManager.RoleExistsAsync(rolename);
                if (!roleExists)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = rolename;
                    await roleManager.CreateAsync(role);
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ClientContext clientContext, RoleManager<IdentityRole> roleManager) //step 7.2 & 31.6 & 38.6
        {
            //step 7.2
            clientContext.Database.EnsureCreated();

            //step 38.6
            //CreateRoles(roleManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            //step 32.6
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
