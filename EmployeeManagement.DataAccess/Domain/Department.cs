using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Domain;

public class Department : BaseEntity
{
    public Department()
    {
        Employees = new List<Employee>();
    }
    public ICollection<Employee> Employees { get; set; }
}
