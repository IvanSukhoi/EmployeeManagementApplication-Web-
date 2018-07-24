using EmployeeManagement.DataEF.Enums;

namespace EmployeeManagement.Domain.Models
{
    public class SettingsModel
    {
        public int UserId { get; set; }
        public Theme Theme { get; set; }
        public Language Language { get; set; }
        public UserModel User { get; set; }
    }
}
