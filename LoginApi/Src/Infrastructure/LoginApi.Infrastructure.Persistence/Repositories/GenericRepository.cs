using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs;
using HyBrForex.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HyBrForex.Infrastructure.Persistence.Repositories;

public class GenericRepository<T>(DbContext dbContext) : IGenericRepository<T> where T : class
{
    public virtual async Task<T> GetByIdAsync(object id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Update(T entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            dbContext.Attach(entity);
        }
        dbContext.Entry(entity).State = EntityState.Modified;
    }
    //public void Update(T entity)
    //{

    //    var entry = dbContext.Entry(entity);

    //    if (entry.State == EntityState.Detached)
    //    {
    //        // Check if an entity with the same key is already tracked
    //        var key = dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.First();
    //        var keyValue = key.PropertyInfo.GetValue(entity);

    //        var tracked = dbContext.ChangeTracker.Entries<T>()
    //            .FirstOrDefault(e =>
    //                key.PropertyInfo.GetValue(e.Entity).Equals(keyValue));

    //        if (tracked != null)
    //        {
    //            // Update the tracked entity with the new values
    //            dbContext.Entry(tracked.Entity).CurrentValues.SetValues(entity);
    //        }
    //        else
    //        {
    //            // Attach and mark as modified if not tracked
    //            dbContext.Attach(entity);
    //            entry.State = EntityState.Modified;
    //        }
    //    }
    //}

    public void Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await dbContext
            .Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllWithChildAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbContext.Set<T>().AsNoTracking();

        // Apply Includes
        foreach (var include in includes) query = query.Include(include);

        return await query.ToListAsync();
    }

    public async Task<List<T>> GetByIdChildAsync(Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbContext.Set<T>();

        // Apply Includes
        foreach (var include in includes) query = query.Include(include);

        // Apply Filter if provided
        if (filter != null) query = query.Where(filter);

        return await query.ToListAsync();
    }

    protected async Task<PaginationResponseDto<TEntity>> Paged<TEntity>(IQueryable<TEntity> query, int pageNumber,
        int pageSize) where TEntity : class
    {
        var count = await query.CountAsync();

        var pagedResult = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return new PaginationResponseDto<TEntity>(pagedResult, count, pageNumber, pageSize);
    }

    public void RemoveRange(ICollection<T> entity)
    {
        dbContext.Set<T>().RemoveRange(entity);
    }
}