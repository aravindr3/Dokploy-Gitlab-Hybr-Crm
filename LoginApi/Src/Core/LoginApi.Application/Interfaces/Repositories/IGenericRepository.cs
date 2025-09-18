using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HyBrForex.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(object id);


    Task<List<T>> GetByIdChildAsync(Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] includes);

    Task<IReadOnlyList<T>> GetAllAsync();

    Task<IReadOnlyList<T>> GetAllWithChildAsync(params Expression<Func<T, object>>[] includes);

    Task<T> AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    void RemoveRange(ICollection<T> entity);

   

}