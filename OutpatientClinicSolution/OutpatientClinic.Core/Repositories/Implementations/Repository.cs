using Microsoft.EntityFrameworkCore;
using OutpatientClinic.Core.Repositories.Interfaces;
using System.Linq.Expressions;

namespace OutpatientClinic.Core.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //Injections
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;


        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbSet.AsNoTracking().ToListAsync();

        public async Task<T> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id) ?? throw new InvalidOperationException($"Entity with id {id} not found.");


        public async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public void Update(T entity) =>
            _dbSet.Update(entity);

        public void Delete(T entity) =>
            _dbSet.Remove(entity);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.AnyAsync(predicate);

        public async Task<int> CountAsync() =>
            await _dbSet.CountAsync();

        public IQueryable<T> Query() =>
            _dbSet.AsQueryable();
    }
}
