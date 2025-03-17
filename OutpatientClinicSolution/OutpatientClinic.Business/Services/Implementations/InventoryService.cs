using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync() =>
            await _unitOfWork.Repository<Inventory>().GetAllAsync();

        public async Task<Inventory> GetInventoryByIdAsync(int id) =>
            await _unitOfWork.Repository<Inventory>().GetByIdAsync(id);

        public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
        {
            await _unitOfWork.Repository<Inventory>().AddAsync(inventory);
            await _unitOfWork.CompleteAsync();
            return inventory;
        }

        public async Task<bool> UpdateInventoryAsync(Inventory inventory)
        {
            _unitOfWork.Repository<Inventory>().Update(inventory);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteInventoryAsync(int id)
        {
            var inv = await _unitOfWork.Repository<Inventory>().GetByIdAsync(id);
            if (inv == null)
                return false;
            _unitOfWork.Repository<Inventory>().Delete(inv);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Inventory>> FindInventoriesAsync(string itemName) =>
            await _unitOfWork.Repository<Inventory>().FindAsync(i => i.ItemName.Contains(itemName));

        public async Task<int> CountInventoriesAsync() =>
            await _unitOfWork.Repository<Inventory>().CountAsync();

        public async Task<Inventory?> GetInventoryByItemNameAsync(string itemName)
        {
            var invs = await _unitOfWork.Repository<Inventory>().FindAsync(i => i.ItemName == itemName);
            return invs.FirstOrDefault();
        }
    }
}
