using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.WebUI.Mappings;
using System.Web.Mvc;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.WebUI.Areas.API.Controllers;
using FromAssembly = Castle.Windsor.Installer.FromAssembly;

namespace EmployeeManagement.WebUI.DI
{
    public class CastleWindsor : IWindsorInstaller
    {
        private static IWindsorContainer _container;

        public static void Setup()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ManagementContext>().LifeStyle.PerWebRequest);

            container.Register(Component.For<IDepartmentService>().ImplementedBy<DepartmentService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IEmployeeService>().ImplementedBy<EmployeeService>().LifeStyle.PerWebRequest);

            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifestylePerWebRequest());
            container.Register(Component.For<ISettingsService>().ImplementedBy<SettingsService>().LifestylePerWebRequest());

            container.Register(Component.For<IMapperWrapper>().ImplementedBy<MapperWrapper>().LifeStyle.Singleton);

            container.Register(Component.For<EmployeeController>().LifestylePerWebRequest());
            container.Register(Component.For<UserController>().LifestylePerWebRequest());
            container.Register(Component.For<SettingsController>().LifestylePerWebRequest());
            container.Register(Component.For<DepartmentController>().LifestylePerWebRequest());

            container.Register(AllTypes.FromThisAssembly().BasedOn<IController>().If(t => t.Name.EndsWith("Controller")).LifestylePerWebRequest());
        }

        public static IWindsorContainer GetWindsorContainer()
        {
            return _container;
        }

        public static void Dispose()
        {
            _container.Dispose();
        }
    }
}
