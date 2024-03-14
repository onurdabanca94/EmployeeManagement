using EmployeeManagement.Business.Abstract;
using EmployeeManagement.Core.Abstract;
using EmployeeManagement.DataAccess.Domain;

namespace EmployeeManagement.Business.Concrete;

public class EmployeeService : IEmployeeService
{
    private readonly IGenericRepository<Employee> _employeeRepository;
    public EmployeeService(IGenericRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public async Task AddAsync(Employee entity)
    {
        await _employeeRepository.AddAsync(entity).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Employee entity)
    {
        await _employeeRepository.DeleteAsync(entity).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _employeeRepository.GetAllAsync().ConfigureAwait(false);
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _employeeRepository.GetByIdAsync(id).ConfigureAwait(false);
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return new Employee();
    }

    public IEnumerable<Employee> GetListByExpressionAsync(Func<Employee, bool> expression)
    {
        return _employeeRepository.GetListByExpressionAsync(expression);
    }

    public async Task UpdateAsync(Employee entity)
    {
        await _employeeRepository.UpdateAsync(entity).ConfigureAwait(false);
    }
}
