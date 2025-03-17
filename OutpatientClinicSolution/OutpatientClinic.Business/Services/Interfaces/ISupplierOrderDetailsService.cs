using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface ISupplierOrderDetailsService
    {
        Task<IEnumerable<SupplierOrderDetail>> GetAllSupplierOrderDetailsAsync();
        Task<SupplierOrderDetail> GetSupplierOrderDetailsByIdAsync(int id);
        Task<SupplierOrderDetail> CreateSupplierOrderDetailsAsync(SupplierOrderDetail details);
        Task<bool> UpdateSupplierOrderDetailsAsync(SupplierOrderDetail details);
        Task<bool> DeleteSupplierOrderDetailsAsync(int id);
        Task<int> CountSupplierOrderDetailsAsync();
    }
}
