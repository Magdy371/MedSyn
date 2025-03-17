using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetAllStaffAsync();
        Task<Staff> GetStaffByIdAsync(int id);
        Task<Staff> CreateStaffAsync(Staff staff);
        Task<bool> UpdateStaffAsync(Staff staff);
        Task<bool> DeleteStaffAsync(int id);
        Task<IEnumerable<Staff>> FindStaffAsync(string name);
        Task<int> CountStaffAsync();
    }
}
