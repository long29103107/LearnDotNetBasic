using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Entities;
using TodoList.Authentication;

namespace TodoList.DB
{
    public class SeedData
    {
        public static async void Inittialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<TodoContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            context.Database.EnsureCreated();  
            if(!context.Roles.Any())
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = "Admin",
                    ConcurrencyStamp = "1",
                };
                await roleManager.CreateAsync(role);

                role = new IdentityRole()
                {
                    Name = "Member",
                    ConcurrencyStamp = "2",
                };
                await roleManager.CreateAsync(role);
            }    
            if(!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "admin@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "123456@Aa");

                if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                }
            }
        }
    }
}
