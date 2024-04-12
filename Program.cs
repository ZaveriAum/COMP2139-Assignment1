 using COMP2139_Assignment1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using COMP2139_Assignment1.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using COMP2139_Assignment1.Areas.NorthPole.Models;

namespace COMP2139_Assignment1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages();
			builder.Services.AddDbContext<ApplicationDbContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<NorthPoleUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<IEmailSender, EmailSender>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("Home/NotFound?statusCode=[0]");
            }

            using var scope = app.Services.CreateScope();
            var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();

            try
            {
                // Get Services needed for role seeding
                // scope.ServiceProvider - used to access instances of registered services
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<NorthPoleUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // seed roles
                await ContextSeed.SeedRolesAsync(userManager, roleManager);
                // seed superAdmin
                await ApplicationDbInitializer.SeedAsync(app,userManager);
                await ContextSeed.SuperSeedRoleAsync(userManager, roleManager);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(e, "An error occurred when attempting to seed the roles for the system.");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();
            //app.MapControllerRoute(
            //    name: "area",
            //    pattern: "{area:exists}/{controller=Project}/{action=Index}/{id?}");
            // the above is routing to the area in program.cs

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
