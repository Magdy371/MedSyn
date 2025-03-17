using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetAllInventoriesAsync();
        Task<Inventory> GetInventoryByIdAsync(int id);
        Task<Inventory> CreateInventoryAsync(Inventory inventory);
        Task<bool> UpdateInventoryAsync(Inventory inventory);
        Task<bool> DeleteInventoryAsync(int id);
        Task<IEnumerable<Inventory>> FindInventoriesAsync(string itemName);
        Task<int> CountInventoriesAsync();
        Task<Inventory?> GetInventoryByItemNameAsync(string itemName);
    }
}
