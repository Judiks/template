using Microsoft.Extensions.DependencyInjection;
using THD.Domain.Helpers;
using THD.Domain.Repositories;
using THD.Domain.Services;
using THD.Domain.Services.Interfaces;
using THD.Infrastructure.Helpers;
using THD.Infrastructure.Repositories;

namespace THD.Infrastructure.Extension
{
    public static class DomainDependenciesExtension
    {
        public static void AddDomainDependencies(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped<IAccountService, AccountService>();

            //Helpers
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddSingleton<IMemoryCacheHelper, MemoryCacheHelper>();
        }
    }
}
