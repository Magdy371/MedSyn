using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync() =>
            await _unitOfWork.Repository<Department>().GetAllAsync();

        public async Task<Department> GetDepartmentByIdAsync(int id) =>
            await _unitOfWork.Repository<Department>().GetByIdAsync(id);

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            await _unitOfWork.Repository<Department>().AddAsync(department);
            await _unitOfWork.CompleteAsync();
            return department;
        }

        public async Task<bool> UpdateDepartmentAsync(Department department)
        {
            _unitOfWork.Repository<Department>().Update(department);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var dept = await _unitOfWork.Repository<Department>().GetByIdAsync(id);
            if (dept == null)
                return false;
            _unitOfWork.Repository<Department>().Delete(dept);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Department>> FindDepartmentsAsync(string departmentName) =>
            await _unitOfWork.Repository<Department>().FindAsync(d => d.DepartmentName.Contains(departmentName));

        public async Task<int> CountDepartmentsAsync() =>
            await _unitOfWork.Repository<Department>().CountAsync();

        public async Task<Department?> GetDepartmentByNameAsync(string name)
        {
            var departments = await _unitOfWork.Repository<Department>().FindAsync(d => d.DepartmentName == name);
            return departments.FirstOrDefault();
        }
    }
}
