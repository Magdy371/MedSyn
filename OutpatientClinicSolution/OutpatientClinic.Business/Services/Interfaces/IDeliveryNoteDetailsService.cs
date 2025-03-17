using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IDeliveryNoteDetailsService
    {
        Task<IEnumerable<DeliveryNoteDetail>> GetAllDeliveryNoteDetailsAsync();
        Task<DeliveryNoteDetail> GetDeliveryNoteDetailsByIdAsync(int id);
        Task<DeliveryNoteDetail> CreateDeliveryNoteDetailsAsync(DeliveryNoteDetail details);
        Task<bool> UpdateDeliveryNoteDetailsAsync(DeliveryNoteDetail details);
        Task<bool> DeleteDeliveryNoteDetailsAsync(int id);
        Task<int> CountDeliveryNoteDetailsAsync();
    }
}
