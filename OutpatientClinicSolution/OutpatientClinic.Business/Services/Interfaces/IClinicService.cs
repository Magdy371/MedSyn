using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IClinicService
    {
        Task<IEnumerable<Clinic>> GetAllClinicAsync();
        Task<Clinic> GetClinicByIdAsync(int id);
        Task<Clinic> CreateClinicAsync(Clinic clinic);
        Task<bool> UpdateClinicAsync(Clinic clinic);
        Task<bool> DeleteClinicAsync(int id);
        Task<IEnumerable<Clinic>> FindClinicsAsync(string name);
        Task<int> CountClinicsAsync();
        Task<Clinic> GetClinicByNameAsync(string name);
    }
}
