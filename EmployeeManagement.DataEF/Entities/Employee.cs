using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagement.DataEF.Enums;

namespace EmployeeManagement.DataEF.Entities
{
    [Table("Employee")]
    public class Employee
    {
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MidleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public Profession Profession { get; set; }

        public Position? Position { get; set; }

        public Sex Sex { get; set; }

        public int? ManagerID { get; set; }

        public int ID { get; set; }

        public virtual Department Department { get; set; }
    }
}


