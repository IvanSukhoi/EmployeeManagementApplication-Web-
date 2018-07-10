using System.Web.Http;

namespace EmployeeManagement.WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api",
                defaults: new
                {
                    controller = "Employee",
                    action = "GetAll",
                }
            );

            config.Routes.MapHttpRoute(
                name: "",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Employee",
                    id = RouteParameter.Optional
                }
            );
        }
    }
}