﻿using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Core.Abstract;
using EmployeeManagement.Infrastructure.Context;
using System.Linq.Expressions;

namespace EmployeeManagement.Core.Concrete;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BaseDbContext _context;
    public GenericRepository(BaseDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(T entity)
    {
        await _context.AddAsync(entity).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync().ConfigureAwait(false);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id).ConfigureAwait(false);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id).ConfigureAwait(false);
    }

    public IEnumerable<T> GetListByExpressionAsync(Func<T, bool> expression)
    {
        return _context.Set<T>().Where(expression).ToList();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(expression).ConfigureAwait(false);
    }
}
