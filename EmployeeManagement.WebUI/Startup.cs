﻿using System;
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
        private ValidationHelper _validationHelper;

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

            services.AddMvc();

            services.AddElmah(options =>
            {
                options.Path = "/elmah";
            });
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
                var role = context.User.Claims.SingleOrDefault(x => x.Type == "Admin")?.Value;

                return role == null;

            }, Handle);

            app.UseElmah();
            app.UseMvc();
        }

        private static void Handle(IApplicationBuilder app)
        {
            app.Run(async context => { await context.Response.WriteAsync("Invalid access"); });
        }

        public void ConfigureJwtAuthServer(IServiceCollection services)
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                _validationHelper = new ValidationHelper();
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = _validationHelper.ValidationSecurityStamp
                };
                options.TokenValidationParameters = tokenValidationParameters;
            });

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
