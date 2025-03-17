using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface ISupplierOrderService
    {
        Task<IEnumerable<SupplierOrder>> GetAllSupplierOrdersAsync();
        Task<SupplierOrder> GetSupplierOrderByIdAsync(int id);
        Task<SupplierOrder> CreateSupplierOrderAsync(SupplierOrder order);
        Task<bool> UpdateSupplierOrderAsync(SupplierOrder order);
        Task<bool> DeleteSupplierOrderAsync(int id);
        Task<IEnumerable<SupplierOrder>> FindSupplierOrdersAsync(string status);
        Task<int> CountSupplierOrdersAsync();
    }
}
