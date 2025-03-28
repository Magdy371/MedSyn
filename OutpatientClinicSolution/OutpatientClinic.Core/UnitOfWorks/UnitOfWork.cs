using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OutpatientClinic.Core.Repositories.Implementations;
using OutpatientClinic.Core.Repositories.Interfaces;
using OutpatientClinic.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Core.UnitOfWorks
{
    /// <summary>
    /// Represents the Unit of Work pattern for managing repositories and transactions.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OutpatientClinicDbContext _context;
        private readonly Dictionary<Type, object> _repositories;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UnitOfWork(OutpatientClinicDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Gets the repository for the specified entity type.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <returns>The repository for the specified entity type.</returns>
        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (_repositories.ContainsKey(type))
            {
                return (IRepository<T>)_repositories[type];
            }

            var repositoryInstance = new Repository<T>(_context);
            _repositories.Add(type, repositoryInstance);
            return repositoryInstance;
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> CompleteAsync() =>
            await _context.SaveChangesAsync();

       

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="UnitOfWork"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Releases all resources used by the <see cref="UnitOfWork"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync() =>
        await _context.Database.BeginTransactionAsync();
    }
}
