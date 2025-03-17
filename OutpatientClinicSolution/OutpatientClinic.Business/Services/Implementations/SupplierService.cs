using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync() =>
            await _unitOfWork.Repository<Supplier>().GetAllAsync();

        public async Task<Supplier> GetSupplierByIdAsync(int id) =>
            await _unitOfWork.Repository<Supplier>().GetByIdAsync(id);

        public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
        {
            await _unitOfWork.Repository<Supplier>().AddAsync(supplier);
            await _unitOfWork.CompleteAsync();
            return supplier;
        }

        public async Task<bool> UpdateSupplierAsync(Supplier supplier)
        {
            _unitOfWork.Repository<Supplier>().Update(supplier);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supp = await _unitOfWork.Repository<Supplier>().GetByIdAsync(id);
            if (supp == null)
                return false;
            _unitOfWork.Repository<Supplier>().Delete(supp);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Supplier>> FindSuppliersAsync(string name) =>
            await _unitOfWork.Repository<Supplier>().FindAsync(s => s.Name.Contains(name));

        public async Task<int> CountSuppliersAsync() =>
            await _unitOfWork.Repository<Supplier>().CountAsync();

        public async Task<Supplier?> GetSupplierByNameAsync(string name)
        {
            var suppliers = await _unitOfWork.Repository<Supplier>().FindAsync(s => s.Name == name);
            return suppliers.FirstOrDefault();
        }
    }
}
