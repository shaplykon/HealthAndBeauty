using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Services
{
    public static class RoleInitializer
    {
        internal static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string courierEmail = "courier@gmail.com";
            string managerEmail = "manager@gmail.com";

            string password = "Admin1_";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("manager") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("manager"));
            }
            if (await roleManager.FindByNameAsync("courier") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("courier"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                IdentityUser admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }

               
            }

            if (await userManager.FindByNameAsync(courierEmail) == null)
            {
                IdentityUser user = new IdentityUser { Email = courierEmail, UserName = courierEmail };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "courier");
                }
            }

            if (await userManager.FindByNameAsync(managerEmail) == null)
            {
                IdentityUser user = new IdentityUser { Email = managerEmail, UserName = managerEmail };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "manager");
                }


            }
        }
    }
}
