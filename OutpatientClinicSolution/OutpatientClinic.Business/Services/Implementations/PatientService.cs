using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Context;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OutpatientClinicDbContext _context;

        public PatientService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, OutpatientClinicDbContext context)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync() =>
            (await _unitOfWork.Repository<Patient>().GetAllAsync()) ?? new List<Patient>();
        //await _unitOfWork.Repository<Patient>().GetAllAsync();

        // Inside PatientService.cs
        public async Task<IEnumerable<Patient>> GetActivePatientsAsync()
        {
            var patients = await _unitOfWork.Repository<Patient>().GetAllAsync();
            return patients.Where(p => !(p.IsDeleted ?? false));
        }

        public async Task<Patient> GetPatientByIdAsync(int id) =>
            await _unitOfWork.Repository<Patient>().GetByIdAsync(id);

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            await _unitOfWork.Repository<Patient>().AddAsync(patient);
            await _unitOfWork.CompleteAsync();
            return patient;
        }

        public async Task<Patient?> GetPatientByUserIdAsync(string userId)
        {
            return await _unitOfWork.Repository<Patient>()
                .FindAsync(p => p.UserId == userId)
                .ContinueWith(t => t.Result.FirstOrDefault());
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            _unitOfWork.Repository<Patient>().Update(patient);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _unitOfWork.Repository<Patient>().GetByIdAsync(id);
            if (patient == null)
                return false;
            _unitOfWork.Repository<Patient>().Delete(patient);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Patient>> FindPatientsAsync(string name) =>
            await _unitOfWork.Repository<Patient>().FindAsync(p => p.FirstName.Contains(name) || p.LastName.Contains(name));

        public async Task<int> CountPatientsAsync() =>
            await _unitOfWork.Repository<Patient>().CountAsync();

        public async Task<IEnumerable<Patient>> GetPatientsByPrimaryDoctorAsync(int doctorId) =>
            await _unitOfWork.Repository<Patient>().FindAsync(p => p.PrimaryDoctorId == doctorId);

        public async Task<IEnumerable<PatientSearchResult>> SearchPatientsAsync(string term)
        {
            return await _userManager.Users
                .Where(u => u.FullName.Contains(term) || u.Email.Contains(term))
                .Join(_context.Patients,
                    user => user.Id,
                    patient => patient.UserId,
                    (user, patient) => new PatientSearchResult
                    {
                        PatientId = patient.PatientId,
                        FullName = user.FullName,
                        Email = user.Email
                    })
                .ToListAsync();
        }
    }

    public class PatientSearchResult
    {
        public int PatientId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}
