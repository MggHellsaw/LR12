using Microsoft.EntityFrameworkCore;
using LR12.Models;

namespace LR12
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
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<CompanyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationContext>();
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { FirstName = "Name 1", LastName = "LastName 1", Age = 44 },
                        new User { FirstName = "Name 2", LastName = "LastName 2", Age = 34 },
                        new User { FirstName = "Name 3", LastName = "LastName 3", Age = 23 }
                    );
                    context.SaveChanges();
                }

                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    Console.WriteLine($"Id: {user.Id}, Name: {user.FirstName} {user.LastName}, Age: {user.Age}");
                }
            }
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<CompanyContext>();
                if (!context.Companies.Any())
                {
                    context.Companies.AddRange(
                         new Company { Name = "Company 1", Location = "Location 1" },
                         new Company { Name = "Company 2", Location = "Location 2" },
                         new Company { Name = "Company 3", Location = "Location 3" },
                         new Company { Name = "Company 4", Location = "Location 4" },
                         new Company { Name = "Company 5", Location = "Location 5" }
                     );
                    context.SaveChanges();
                }
            }
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
