using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class DeliveryNoteDetailsService : IDeliveryNoteDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeliveryNoteDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DeliveryNoteDetail>> GetAllDeliveryNoteDetailsAsync() =>
            await _unitOfWork.Repository<DeliveryNoteDetail>().GetAllAsync();

        public async Task<DeliveryNoteDetail> GetDeliveryNoteDetailsByIdAsync(int id) =>
            await _unitOfWork.Repository<DeliveryNoteDetail>().GetByIdAsync(id);

        public async Task<DeliveryNoteDetail> CreateDeliveryNoteDetailsAsync(DeliveryNoteDetail details)
        {
            await _unitOfWork.Repository<DeliveryNoteDetail>().AddAsync(details);
            await _unitOfWork.CompleteAsync();
            return details;
        }

        public async Task<bool> UpdateDeliveryNoteDetailsAsync(DeliveryNoteDetail details)
        {
            _unitOfWork.Repository<DeliveryNoteDetail>().Update(details);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteDeliveryNoteDetailsAsync(int id)
        {
            var details = await _unitOfWork.Repository<DeliveryNoteDetail>().GetByIdAsync(id);
            if (details == null)
                return false;
            _unitOfWork.Repository<DeliveryNoteDetail>().Delete(details);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<int> CountDeliveryNoteDetailsAsync() =>
            await _unitOfWork.Repository<DeliveryNoteDetail>().CountAsync();
    }
}
