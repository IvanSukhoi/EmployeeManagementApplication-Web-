using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.WebUI.FluentValidator;
using FluentValidation.Attributes;

namespace EmployeeManagement.WebUI.Models
{
    [Validator(typeof(RegisterModelValidator))]
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int? ManagerId { get; set; }

        public Sex Sex { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public Position? Position { get; set; }

        public Profession Profession { get; set; }
    }
}