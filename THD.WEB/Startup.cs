using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using THD.Core.Constants;
using THD.Infrastructure.Extension;
using THD.Infrastructure.Helpers;
using THD.Infrastructure.Helpers.MapperProfile;
using THD.Infrastructure.Options;
using THD.WEB.Middlewares;

namespace THD.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddDomainDependencies();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "1.0.0v";
                    document.Info.Title = "THD Simulation";
                    document.Info.Description = "ASP.NET Core web API";
                };
            });
            ConfigureCors(services, Configuration);
            services.Configure<AuthTokenProviderOption>(Configuration.GetSection(typeof(AuthTokenProviderOption).Name));
            services.Configure<List<Attribute>>(Configuration.GetSection("FixedAttributes"));
            var securityKey = Configuration.GetSection("AuthTokenProviderOption:JwtKey").Value;
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddAuthOptions(securityKey);
            services.AddDbContext(connectionString);
            DBInitializerHelper.InizializationAsync(services).Wait();
            ConfigureAutomapper(services);
            services.Configure<List<Attribute>>(Configuration.GetSection("FixedAttributes"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            loggerFactory.AddFile(Configuration.GetSection("Logging"));
            app.UseCors("OriginPolicy");
            app.UseAuthentication();
            app.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "wwwroot";
#if DEBUG
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
#endif
            });
        }

        private void ConfigureCors(IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection corsOptions = configuration.GetSection("Cors");
            string origins = corsOptions["Origins"];
            services.AddCors(options =>
            {
                options.AddPolicy("OriginPolicy", builder =>
                {
                    builder.WithOrigins(origins.Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials().WithExposedHeaders(ExceptionConstants.TokenExpiredHeader, ExceptionConstants.InvalidRefresh);
                });
            });
        }

        public static void ConfigureAutomapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = true;
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
