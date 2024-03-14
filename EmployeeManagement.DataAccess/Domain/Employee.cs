using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Domain;

public class Employee : BaseEntity
{
    public Employee()
    {
        DepartmentName = new Department();
    }
    public int DepartmentId { get; set; }
    public Department DepartmentName { get; set; }
}
