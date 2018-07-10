using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagement.DataEF.Enums;

namespace EmployeeManagement.DataEF.Entities
{
    public class Settings
    {
        [Key, Required]
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }

        public Theme Topic { get; set; }

        public Language Language { get; set; }

        public virtual User User { get; set; }
    }
}