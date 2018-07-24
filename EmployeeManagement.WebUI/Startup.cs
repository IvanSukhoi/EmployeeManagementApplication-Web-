using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.DbProviders;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.DataEF.Repositories;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Services;
using EmployeeManagement.WebUI.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.WebUI
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
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddDbContext<ManagementContext>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IMapperWrapper, MapperWrapper>();

            services.AddScoped<IQueryableDbProvider, QueryableDbProvider>();
            services.AddScoped<IUpdateDbProvider, UpdateDbProvider>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc( /*routes =>
            {
                  routes.MapRoute("default", "{controller=Employee}/{action=GetAll}");
                  routes.MapRoute("", "api/{controller=Employee}/{action}/{id?}");
            }*/);
        }
    }
}
