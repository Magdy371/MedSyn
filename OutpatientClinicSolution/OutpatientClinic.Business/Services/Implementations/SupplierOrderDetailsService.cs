using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class SupplierOrderDetailsService : ISupplierOrderDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierOrderDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SupplierOrderDetail>> GetAllSupplierOrderDetailsAsync() =>
            await _unitOfWork.Repository<SupplierOrderDetail>().GetAllAsync();

        public async Task<SupplierOrderDetail> GetSupplierOrderDetailsByIdAsync(int id) =>
            await _unitOfWork.Repository<SupplierOrderDetail>().GetByIdAsync(id);

        public async Task<SupplierOrderDetail> CreateSupplierOrderDetailsAsync(SupplierOrderDetail details)
        {
            await _unitOfWork.Repository<SupplierOrderDetail>().AddAsync(details);
            await _unitOfWork.CompleteAsync();
            return details;
        }

        public async Task<bool> UpdateSupplierOrderDetailsAsync(SupplierOrderDetail details)
        {
            _unitOfWork.Repository<SupplierOrderDetail>().Update(details);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteSupplierOrderDetailsAsync(int id)
        {
            var details = await _unitOfWork.Repository<SupplierOrderDetail>().GetByIdAsync(id);
            if (details == null)
                return false;
            _unitOfWork.Repository<SupplierOrderDetail>().Delete(details);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<int> CountSupplierOrderDetailsAsync() =>
            await _unitOfWork.Repository<SupplierOrderDetail>().CountAsync();
    }
}
