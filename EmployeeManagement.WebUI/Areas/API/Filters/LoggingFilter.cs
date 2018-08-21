using System;
using System.Linq;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace EmployeeManagement.WebUI.Areas.API.Filters
{
    public class LoggingFilterAttribute : Attribute, IActionFilter
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "userId")?.Value ?? "Anonymous";

            _logger.Info("Request:" + Environment.NewLine + 
                         "Method: " + context.HttpContext.Request.Method + Environment.NewLine +
                         "URI: " + context.HttpContext.Request.GetUri() + Environment.NewLine +
                         "UserId: " + userId + Environment.NewLine +
                         "Controller: " + context.Controller);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.Info("Response:" + Environment.NewLine 
                                     + "StatusCode: " + context.HttpContext.Response.StatusCode);
        }
    }
}
