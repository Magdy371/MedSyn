using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PrescriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync() =>
            await _unitOfWork.Repository<Prescription>().GetAllAsync();

        public async Task<Prescription> GetPrescriptionByIdAsync(int id) =>
            await _unitOfWork.Repository<Prescription>().GetByIdAsync(id);

        public async Task<Prescription> CreatePrescriptionAsync(Prescription prescription)
        {
            await _unitOfWork.Repository<Prescription>().AddAsync(prescription);
            await _unitOfWork.CompleteAsync();
            return prescription;
        }

        public async Task<bool> UpdatePrescriptionAsync(Prescription prescription)
        {
            _unitOfWork.Repository<Prescription>().Update(prescription);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeletePrescriptionAsync(int id)
        {
            var pres = await _unitOfWork.Repository<Prescription>().GetByIdAsync(id);
            if (pres == null) return false;
            _unitOfWork.Repository<Prescription>().Delete(pres);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<int> CountPrescriptionsAsync() =>
            await _unitOfWork.Repository<Prescription>().CountAsync();

        public async Task<IEnumerable<Prescription>> FindPrescriptionsByMedicalNameAsync(string medicalName) =>
            await _unitOfWork.Repository<Prescription>().FindAsync(p => p.MedicalName.Contains(medicalName));
    }
}
