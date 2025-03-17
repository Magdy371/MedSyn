using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync();
        Task<MedicalRecord> GetMedicalRecordByIdAsync(int id);
        Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord record);
        Task<bool> UpdateMedicalRecordAsync(MedicalRecord record);
        Task<bool> DeleteMedicalRecordAsync(int id);
        Task<int> CountMedicalRecordsAsync();
        Task<MedicalRecord?> GetMedicalRecordByAppointmentAsync(int appointmentId);
    }
}
