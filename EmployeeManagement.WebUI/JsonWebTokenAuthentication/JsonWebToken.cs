using System;

namespace EmployeeManagement.WebUI.JsonWebTokenAuthentication
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
    }
}