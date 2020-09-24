using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using THD.Domain.Entities;
using THD.Domain.Entities.Enums;

namespace THD.Infrastructure.Helpers
{
    public static class DBInitializerHelper
    {
        public static async Task InizializationAsync(IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                if ((context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                {
                    UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    await CreateRolesAsync(roleManager);
                    await CreateUserAsync(userManager);
                }
            }
        }

        private static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            IEnumerable<UserRole> allRoles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();
            foreach (UserRole role in allRoles)
            {
                if (!roleManager.Roles.Any(r => r.Name == role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                        NormalizedName = role.ToString().ToUpper()
                    });
                }
            }
        }

        private static async Task CreateUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any(u => u.Email == "admin@mail.com"))
            {
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@mail.com";
                user.FirstName = "Admin";
                user.LastName = "Admin";
                string password = userManager.PasswordHasher.HashPassword(user, "Sysadmin123!");
                user.PasswordHash = password;
                await userManager.CreateAsync(user);
                string roleToCreate = UserRole.SuperAdmin.ToString();
                await userManager.AddToRoleAsync(user, roleToCreate);
            }
            if (!userManager.Users.Any(u => u.Email == "roryliu@gmail.com"))
            {
                var user = new ApplicationUser();
                user.UserName = "RoryLiu";
                user.Email = "roryliu@gmail.com";
                user.FirstName = "Rory";
                user.LastName = "Liu";
                string password = userManager.PasswordHasher.HashPassword(user, "RoryLiuPassword1");
                user.PasswordHash = password;
                await userManager.CreateAsync(user);
                string roleToCreate = UserRole.SuperAdmin.ToString();
                await userManager.AddToRoleAsync(user, roleToCreate);
            }
            if (!userManager.Users.Any(u => u.Email == "admin2@mail.com"))
            {
                var user = new ApplicationUser();

                user.UserName = "admin2";
                user.Email = "admin2@mail.com";
                user.FirstName = "admin2";
                user.LastName = "admin2";
                string password = userManager.PasswordHasher.HashPassword(user, "Sysadmin123!");
                user.PasswordHash = password;
                await userManager.CreateAsync(user);
                string roleToCreate = UserRole.Admin.ToString();
                await userManager.AddToRoleAsync(user, roleToCreate);
            }
        }
    }
}
