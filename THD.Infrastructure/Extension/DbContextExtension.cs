using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using THD.Domain.Entities;

namespace THD.Infrastructure.Extension
{
    public static class DbContextExtension
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            var assemblyName = Assembly.GetAssembly(typeof(ApplicationDbContext)).GetName().Name;
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(assemblyName));
            }, ServiceLifetime.Scoped);

            services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
