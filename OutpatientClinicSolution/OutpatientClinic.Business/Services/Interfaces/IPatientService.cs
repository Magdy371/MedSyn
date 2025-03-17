using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task<Patient> CreatePatientAsync(Patient patient);
        Task<bool> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int id);
        Task<IEnumerable<Patient>> FindPatientsAsync(string name);
        Task<int> CountPatientsAsync();
        Task<IEnumerable<Patient>> GetPatientsByPrimaryDoctorAsync(int doctorId);
    }
}
