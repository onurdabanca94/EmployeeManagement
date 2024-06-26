﻿using EmployeeManagement.Business.Abstract;
using EmployeeManagement.Core.Abstract;
using EmployeeManagement.DataAccess.Domain;
using System.Linq.Expressions;

namespace EmployeeManagement.Business.Concrete;

public class DepartmentService : IDepartmentService
{
    private readonly IGenericRepository<Department> _departmentRepository;
    public DepartmentService(IGenericRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task AddAsync(Department entity)
    {
        await _departmentRepository.AddAsync(entity).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Department entity)
    {
        await _departmentRepository.DeleteAsync(entity).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _departmentRepository.GetAllAsync().ConfigureAwait(false);
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _departmentRepository.GetByIdAsync(id).ConfigureAwait(false);
    }

    public async Task<Department?> GetByIdAsync(Guid id)
    {
        return new Department();
    }

    public async Task<Department?> GetFirstOrDefaultAsync(Expression<Func<Department, bool>> expression)
    {
        return await _departmentRepository.GetFirstOrDefaultAsync(expression).ConfigureAwait(false);
    }

    public IEnumerable<Department> GetListByExpressionAsync(Func<Department, bool> expression)
    {
        return _departmentRepository.GetListByExpressionAsync(expression);
    }

    public async Task UpdateAsync(Department entity)
    {
        await _departmentRepository.UpdateAsync(entity).ConfigureAwait(false);
    }
}
