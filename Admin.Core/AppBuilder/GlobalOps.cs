using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.AppBuilder
{

    public static class GlobalOps
    {
        public static async Task CreateApplicationAdministrator(IServiceProvider serviceProvider)
        {
            try
            {
                // retrive instances of the RoleManager and UserManager from the Dependency Container
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                IdentityResult result;
                // add a new Administrator role for the application
                var isRoleExist = await roleManager.RoleExistsAsync("Administrator");
                if (!isRoleExist)
                {
                    // create Administrator Role and add it in Database
                    result = await roleManager.CreateAsync(new IdentityRole("Administrator"));
                }

                // code to create a default user and add it to Administrator Role
                var user = await userManager.FindByEmailAsync("ilkinlikus@gmail.com");
                if (user == null)
                {
                    var defaultUser = new IdentityUser() { UserName = "ilkinlikus@gmail.com", Email = "ilkinlikus@gmail.com" };
                    var regUser = await userManager.CreateAsync(defaultUser, "P@ssw0rd_");
                    await userManager.AddToRoleAsync(defaultUser, "Administrator");
                }
            }
            catch (Exception ex)
            {
                var str = ex.Message;
            }

        }
    }
}
