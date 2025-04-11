using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface ILabTestService
    {
        Task<IEnumerable<LabTest>> GetAllLabTestsAsync();
        Task<LabTest?> GetLabTestByIdAsync(int id); // Updated return type to match the implementation  
        Task<LabTest> CreateLabTestAsync(LabTest labTest);
        Task<bool> UpdateLabTestAsync(LabTest labTest);
        Task<bool> DeleteLabTestAsync(int id);
        Task<int> CountLabTestsAsync();
        Task<IEnumerable<LabTest>> FindLabTestsByPatientAsync(int patientId);
    }
}
