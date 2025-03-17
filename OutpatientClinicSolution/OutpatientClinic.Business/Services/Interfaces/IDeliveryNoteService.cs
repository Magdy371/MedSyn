using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IDeliveryNoteService
    {
        Task<IEnumerable<DeliveryNote>> GetAllDeliveryNotesAsync();
        Task<DeliveryNote> GetDeliveryNoteByIdAsync(int id);
        Task<DeliveryNote> CreateDeliveryNoteAsync(DeliveryNote note);
        Task<bool> UpdateDeliveryNoteAsync(DeliveryNote note);
        Task<bool> DeleteDeliveryNoteAsync(int id);
        Task<int> CountDeliveryNotesAsync();
    }
}
