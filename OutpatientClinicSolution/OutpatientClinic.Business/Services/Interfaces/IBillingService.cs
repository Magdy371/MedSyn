using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IBillingService
    {
        Task<IEnumerable<Billing>> GetAllBillingsAsync();
        Task<Billing> GetBillingByIdAsync(int id);
        Task<Billing> CreateBillingAsync(Billing billing);
        Task<bool> UpdateBillingAsync(Billing billing);
        Task<bool> DeleteBillingAsync(int id);
        Task<int> CountBillingsAsync();
        Task<IEnumerable<Billing>> FindBillingsByStatusAsync(string status);
    }
}
