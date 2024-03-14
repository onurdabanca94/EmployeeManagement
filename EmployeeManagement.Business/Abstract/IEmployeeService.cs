using EmployeeManagement.Core.Abstract;
using EmployeeManagement.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.Abstract;

public interface IEmployeeService : IGenericRepository<Employee>
{
}
