using EmployeeManagement.Core.Abstract;
using EmployeeManagement.DataAccess.Domain;

namespace EmployeeManagement.Business.Abstract;

public interface IUserService : IGenericRepository<User>
{
    Task<User?> LoginAsync(string username, string password);
}
