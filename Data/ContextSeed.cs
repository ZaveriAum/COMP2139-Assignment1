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
                UserName = "Elio",
                Email = "fezollarielio@gmail.com",
                FirstName = "Elio",
                LastName = "Test",
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
                    await userManager.CreateAsync(superUser, "P@ssword12$");
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.Traveler.ToString());
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.SuperAdmin.ToString());
                }
            }
        }
    }
}
