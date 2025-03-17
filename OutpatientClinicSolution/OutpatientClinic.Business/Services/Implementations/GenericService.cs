using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _unitOfWork.Repository<T>().GetAllAsync();

        public async Task<T> GetByIdAsync(int id) =>
            await _unitOfWork.Repository<T>().GetByIdAsync(id);

        public async Task<T> CreateAsync(T entity)
        {
            await _unitOfWork.Repository<T>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _unitOfWork.Repository<T>().Update(entity);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Repository<T>().GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
            _unitOfWork.Repository<T>().Delete(entity);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _unitOfWork.Repository<T>().FindAsync(predicate);

        public async Task<int> CountAsync() =>
            await _unitOfWork.Repository<T>().CountAsync();
    }
}
