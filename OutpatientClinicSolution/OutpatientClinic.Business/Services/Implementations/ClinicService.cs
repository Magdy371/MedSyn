using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class ClinicService : IClinicService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClinicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Clinic> GetEmergencyClinicAsync()
        {
            return await GetClinicByNameAsync("Emergency");
        }
        public async Task<IEnumerable<Clinic>> GetAllClinicsAsync() =>
            await _unitOfWork.Repository<Clinic>().GetAllAsync();

        public async Task<Clinic> GetClinicByIdAsync(int id) =>
            await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);

        public async Task<Clinic> CreateClinicAsync(Clinic clinic)
        {
            await _unitOfWork.Repository<Clinic>().AddAsync(clinic);
            await _unitOfWork.CompleteAsync();
            return clinic;
        }

        public async Task<bool> UpdateClinicAsync(Clinic clinic)
        {
            _unitOfWork.Repository<Clinic>().Update(clinic);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteClinicAsync(int id)
        {
            var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
            if (clinic == null) return false;
            _unitOfWork.Repository<Clinic>().Delete(clinic);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Clinic>> FindClinicsAsync(string clinicName) =>
            await _unitOfWork.Repository<Clinic>().FindAsync(c => c.ClinicName.Contains(clinicName));

        public async Task<int> CountClinicsAsync() =>
            await _unitOfWork.Repository<Clinic>().CountAsync();

        public async Task<Clinic> GetClinicByNameAsync(string name)
        {
            var clinics = await _unitOfWork.Repository<Clinic>().FindAsync(c => c.ClinicName == name);
            return clinics.FirstOrDefault() ?? throw new KeyNotFoundException($"Clinic with name '{name}' not found.");
        }

        async Task<IEnumerable<Clinic>> IClinicService.GetAllClinicAsync()=>
            await _unitOfWork.Repository<Clinic>().GetAllAsync();
    }
}
