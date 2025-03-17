using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription> GetPrescriptionByIdAsync(int id);
        Task<Prescription> CreatePrescriptionAsync(Prescription prescription);
        Task<bool> UpdatePrescriptionAsync(Prescription prescription);
        Task<bool> DeletePrescriptionAsync(int id);
        Task<int> CountPrescriptionsAsync();
        Task<IEnumerable<Prescription>> FindPrescriptionsByMedicalNameAsync(string medicalName);
    }
}
