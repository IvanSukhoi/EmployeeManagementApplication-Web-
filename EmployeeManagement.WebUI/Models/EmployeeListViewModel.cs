using System.Collections.Generic;

namespace EmployeeManagement.WebUI.Models
{
    public class EmployeeListViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory{ get; set; }
        public int ManagerId { get; set; }
    }
}