
using EmployeeManagement.DataEF.Enums;

namespace EmployeeManagement.Domain.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int? ManagerId { get; set; }

        public Sex Sex { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public Profession Profession { get; set; }

        public Position? Position { get; set; }
    }
}
