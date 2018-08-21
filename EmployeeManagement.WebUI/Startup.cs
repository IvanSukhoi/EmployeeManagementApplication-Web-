using System;
using System.Linq;
using System.Text;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.DbProviders;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.Domain.Cache;
using EmployeeManagement.Domain.Identity.Stores;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Domain.Services;
using EmployeeManagement.WebUI.Areas.API.Filters;
using EmployeeManagement.WebUI.Helpers;
using EmployeeManagement.WebUI.Identity;
using EmployeeManagement.WebUI.Interfaces;
using EmployeeManagement.WebUI.JsonWebTokenAuthentication;
using EmployeeManagement.WebUI.Mappings.MapperWrapper;
using EmployeeManagement.WebUI.NLog;
using EmployeeManagement.WebUI.Services;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.WebUI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private TokenValidationHelper _tokenValidationHelper;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ManagementContext>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ISettingsService, SettingsService>();

            services.AddSingleton<IMapperWrapper, MapperWrapper>();

            services.AddScoped<ICacheManager, CacheManager>();
            services.AddScoped<IUserCacheManager, UserCacheManager>();

            services.AddScoped<IQueryableDbProvider, QueryableDbProvider>();
            services.AddScoped<IUpdateDbProvider, UpdateDbProvider>();

            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddIdentity<UserModel, UserRole>()
                .AddUserManager<UserManager>()
                .AddSignInManager<SignInManager>()
                .AddDefaultTokenProviders();

            services.AddTransient<UserStore>();
            services.AddTransient<IRoleStore<UserRole>, RoleStore>();

            ConfigureJwtAuthServer(services);
            ConfigureCoockieAuthServer(services);

            services.AddMvc(options => { options.Filters.Add<LoggingFilterAttribute>(); });

            services.AddElmah(options =>
            {
                options.Path = "/elmah";
            });

            AddPolicy(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.MapWhen(context =>
            {
                if (!context.Request.GetUri().ToString().EndsWith("/elmah")) return false;
                var role = context.User.Claims.SingleOrDefault(x => x.Type == "Admin");

                return role == null;

            }, Handle);

            app.UseElmah();
            app.UseMvc();
        }

        private static void Handle(IApplicationBuilder app)
        {
            app.Run(async context => { await context.Response.WriteAsync("Invalid access"); });
        }

        private void ConfigureJwtAuthServer(IServiceCollection services)
        {
            services.AddScoped<IAccountSevice, AccountService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            services.AddScoped<JsonWebTokenHandler>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["jwt:issuer"],
                ValidAudience = _configuration["jwt:audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["securityKey"])),
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication().AddJwtBearer(options =>
            {
                _tokenValidationHelper = new TokenValidationHelper();
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = _tokenValidationHelper.ValidationSecurityStamp
                };
                options.TokenValidationParameters = tokenValidationParameters;
            });
        }

        private void ConfigureCoockieAuthServer(IServiceCollection services)
        {
            services.AddScoped<ISecurityStampValidator, Helpers.SecurityStampValidator>();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(10);
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/coockie/login");
                options.AccessDeniedPath = new PathString("/coockie/login");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            });
        }

        private void AddPolicy(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.RequireClaim("Admin"));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User",
                    policy => policy.RequireClaim("User"));
            });
        }
    }
}
