using COMP2139_Assignment1.Areas.NorthPole.Models;
using Microsoft.AspNetCore.Identity;

namespace COMP2139_Assignment1.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<NorthPoleUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Traveler.ToString()));
        }
        public static async Task SuperSeedRoleAsync(UserManager<NorthPoleUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var superUser = new NorthPoleUser
            {
                UserName = "SuperAdmin",
                Email = "zanrenoo@gmail.com",
                FirstName = "Elio",
                LastName = "SuperAdmin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Address = "Random Address",
                City = "Mississauga",
                Country = "Canada"
            };
            if (userManager.Users.All(u => u.Id != superUser.Id))
            {
                var user = await userManager.FindByEmailAsync(superUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(superUser, "Passw@rd!23");
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.Traveler.ToString());
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.SuperAdmin.ToString());
                }
            }
        }
    }
}
