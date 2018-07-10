using System.Web.Mvc;
using System.Web.Routing;

namespace EmployeeManagement.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Employee",
                    action = "List",
                    category = (string)null,
                    page = 1,
                    managerId = 0
                },
                new[] { "EmployeeManagement.WebUI.Controllers" }
            );

            routes.MapRoute(null,
                "Page{page}/Manager{managerId}",
                new { controller = "Employee", action = "List", category = (string)null, managerId = 0 },
                new { page = @"\d+" },
                new[] { "EmployeeManagement.WebUI.Controllers" }
            );

            routes.MapRoute(null,
                "{category}/Manager{managerId}",
                new { controller = "Employee", action = "List", page = 1, managerId = 0 },
                new[] { "EmployeeManagement.WebUI.Controllers" }
            );

            routes.MapRoute(null,
                "{category}/{page}/Manager{managerId}",
                new { controller = "Employee", action = "List" },
                new { page = @"\d+" },
                new[] { "EmployeeManagement.WebUI.Controllers" }
            );

            routes.MapRoute(null,
                "Employee{employeeId}",
                new { controller = "Employee", action = "GetManagerEmployee", category = (string)null },
                new[] { "EmployeeManagement.WebUI.Controllers" }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}