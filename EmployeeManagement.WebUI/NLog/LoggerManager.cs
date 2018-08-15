using EmployeeManagement.WebUI.Interfaces;
using NLog;

namespace EmployeeManagement.WebUI.NLog
{
    public class LoggerManager : ILoggerManager
    {
        public Logger Get()
        {
            return LogManager.GetCurrentClassLogger();
        }
    }
}
