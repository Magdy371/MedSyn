using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IInsuranceService
    {
        Task<IEnumerable<Insurance>> GetAllInsurancesAsync();
        Task<Insurance> GetInsuranceByIdAsync(int id);
        Task<Insurance> CreateInsuranceAsync(Insurance insurance);
        Task<bool> UpdateInsuranceAsync(Insurance insurance);
        Task<bool> DeleteInsuranceAsync(int id);
        Task<int> CountInsurancesAsync();
        Task<Insurance?> GetInsuranceByPolicyNumberAsync(string policyNumber);
    }
}
