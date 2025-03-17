using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MedicalRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync() =>
            await _unitOfWork.Repository<MedicalRecord>().GetAllAsync();

        public async Task<MedicalRecord> GetMedicalRecordByIdAsync(int id) =>
            await _unitOfWork.Repository<MedicalRecord>().GetByIdAsync(id);

        public async Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord record)
        {
            await _unitOfWork.Repository<MedicalRecord>().AddAsync(record);
            await _unitOfWork.CompleteAsync();
            return record;
        }

        public async Task<bool> UpdateMedicalRecordAsync(MedicalRecord record)
        {
            _unitOfWork.Repository<MedicalRecord>().Update(record);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteMedicalRecordAsync(int id)
        {
            var rec = await _unitOfWork.Repository<MedicalRecord>().GetByIdAsync(id);
            if (rec == null) return false;
            _unitOfWork.Repository<MedicalRecord>().Delete(rec);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<int> CountMedicalRecordsAsync() =>
            await _unitOfWork.Repository<MedicalRecord>().CountAsync();

        public async Task<MedicalRecord?> GetMedicalRecordByAppointmentAsync(int appointmentId)
        {
            var records = await _unitOfWork.Repository<MedicalRecord>().FindAsync(r => r.AppointmentId == appointmentId);
            return records.FirstOrDefault()?? throw new KeyNotFoundException($"No medical record found with  appointment '{appointmentId}' not found.");
        }
    }
}
