using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class SupplierOrderService : ISupplierOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SupplierOrder>> GetAllSupplierOrdersAsync() =>
            await _unitOfWork.Repository<SupplierOrder>().GetAllAsync();

        public async Task<SupplierOrder> GetSupplierOrderByIdAsync(int id) =>
            await _unitOfWork.Repository<SupplierOrder>().GetByIdAsync(id);

        public async Task<SupplierOrder> CreateSupplierOrderAsync(SupplierOrder order)
        {
            await _unitOfWork.Repository<SupplierOrder>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return order;
        }

        public async Task<bool> UpdateSupplierOrderAsync(SupplierOrder order)
        {
            _unitOfWork.Repository<SupplierOrder>().Update(order);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteSupplierOrderAsync(int id)
        {
            var order = await _unitOfWork.Repository<SupplierOrder>().GetByIdAsync(id);
            if (order == null)
                return false;
            _unitOfWork.Repository<SupplierOrder>().Delete(order);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<SupplierOrder>> FindSupplierOrdersAsync(string status) =>
            await _unitOfWork.Repository<SupplierOrder>().FindAsync(o => o.Status != null && o.Status.Contains(status));

        public async Task<int> CountSupplierOrdersAsync() =>
            await _unitOfWork.Repository<SupplierOrder>().CountAsync();
    }
}
