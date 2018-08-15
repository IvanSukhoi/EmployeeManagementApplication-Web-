using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagement.DataEF.Enums;

namespace EmployeeManagement.DataEF.Entities
{
    [Table("Employee")]
    public class Employee
    {
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public Profession Profession { get; set; }

        public Position? Position { get; set; }

        public Sex Sex { get; set; }

        public int? ManagerId { get; set; }

        [Key, Required]
        public int Id { get; set; }

        public virtual Department Department { get; set; }
    }
}


