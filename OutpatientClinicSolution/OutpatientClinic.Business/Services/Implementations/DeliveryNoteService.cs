using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class DeliveryNoteService : IDeliveryNoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeliveryNoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DeliveryNote>> GetAllDeliveryNotesAsync() =>
            await _unitOfWork.Repository<DeliveryNote>().GetAllAsync();

        public async Task<DeliveryNote> GetDeliveryNoteByIdAsync(int id) =>
            await _unitOfWork.Repository<DeliveryNote>().GetByIdAsync(id);

        public async Task<DeliveryNote> CreateDeliveryNoteAsync(DeliveryNote note)
        {
            await _unitOfWork.Repository<DeliveryNote>().AddAsync(note);
            await _unitOfWork.CompleteAsync();
            return note;
        }

        public async Task<bool> UpdateDeliveryNoteAsync(DeliveryNote note)
        {
            _unitOfWork.Repository<DeliveryNote>().Update(note);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteDeliveryNoteAsync(int id)
        {
            var note = await _unitOfWork.Repository<DeliveryNote>().GetByIdAsync(id);
            if (note == null)
                return false;
            _unitOfWork.Repository<DeliveryNote>().Delete(note);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<int> CountDeliveryNotesAsync() =>
            await _unitOfWork.Repository<DeliveryNote>().CountAsync();
    }
}
