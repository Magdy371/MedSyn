using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OutpatientClinic.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        //Basic CRUD Operations
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddSync(T entity);
        Task<T> UpdateSync(T entity);
        Task<T> DeleteSync(T entity);

        //Query and Helper methods
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        IQueryable<T> Query();

    }
}
