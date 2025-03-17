using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class LabTestService : ILabTestService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LabTestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LabTest>> GetAllLabTestsAsync() =>
            await _unitOfWork.Repository<LabTest>().GetAllAsync();

        public async Task<LabTest> GetLabTestByIdAsync(int id) =>
            await _unitOfWork.Repository<LabTest>().GetByIdAsync(id);

        public async Task<LabTest> CreateLabTestAsync(LabTest labTest)
        {
            await _unitOfWork.Repository<LabTest>().AddAsync(labTest);
            await _unitOfWork.CompleteAsync();
            return labTest;
        }

        public async Task<bool> UpdateLabTestAsync(LabTest labTest)
        {
            _unitOfWork.Repository<LabTest>().Update(labTest);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteLabTestAsync(int id)
        {
            var labTest = await _unitOfWork.Repository<LabTest>().GetByIdAsync(id);
            if (labTest == null)
                return false;
            _unitOfWork.Repository<LabTest>().Delete(labTest);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<int> CountLabTestsAsync() =>
            await _unitOfWork.Repository<LabTest>().CountAsync();

        public async Task<IEnumerable<LabTest>> FindLabTestsByPatientAsync(int patientId) =>
            await _unitOfWork.Repository<LabTest>().FindAsync(l => l.PatientId == patientId);
    }
}
