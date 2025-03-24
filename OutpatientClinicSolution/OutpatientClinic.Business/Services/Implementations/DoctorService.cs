using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync() =>
            await _unitOfWork.Repository<Doctor>().GetAllAsync();

        public async Task<Doctor> GetDoctorByIdAsync(int id) =>
            await _unitOfWork.Repository<Doctor>().GetByIdAsync(id);

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
            //await _unitOfWork.Repository<Doctor>().AddAsync(doctor);
            //await _unitOfWork.CompleteAsync();
            //return doctor;
            await _unitOfWork.Repository<Doctor>().AddAsync(doctor);
            var result = await _unitOfWork.CompleteAsync();
            Console.WriteLine($"SaveChanges returned: {result}"); // Debugging
            if (result == 0)
            {
                throw new Exception("Doctor was not saved to the database.");
            }
            return doctor;
        }

        public async Task<bool> UpdateDoctorAsync(Doctor doctor)
        {
            _unitOfWork.Repository<Doctor>().Update(doctor);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(id);
            if (doctor == null)
                return false;
            _unitOfWork.Repository<Doctor>().Delete(doctor);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Doctor>> FindDoctorsAsync(string specialty) =>
            await _unitOfWork.Repository<Doctor>().FindAsync(d => d.Specialty.Contains(specialty));

        public async Task<int> CountDoctorsAsync() =>
            await _unitOfWork.Repository<Doctor>().CountAsync();

        public async Task<Doctor ?> GetDoctorByLicenseNumberAsync(string licenseNumber)
        {
            var doctors = await _unitOfWork.Repository<Doctor>().FindAsync(d => d.LicenseNumber == licenseNumber);
            return doctors.FirstOrDefault() ?? throw new KeyNotFoundException($"Doctor with licenseNumber '{licenseNumber}' not found."); ;
        }
    }
}
