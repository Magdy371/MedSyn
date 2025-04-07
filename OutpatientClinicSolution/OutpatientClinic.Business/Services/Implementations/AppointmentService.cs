using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _unitOfWork.Repository<Appointment>()
                .Query()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.DoctorNavigation)
                .Include(a => a.Clinic)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id) =>
            await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
            await _unitOfWork.CompleteAsync();
            return appointment;
        }
        // AppointmentService.cs
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _unitOfWork.BeginTransactionAsync();
        }
        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            _unitOfWork.Repository<Appointment>().Update(appointment);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var app = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
            if (app == null)
                return false;
            _unitOfWork.Repository<Appointment>().Delete(app);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Appointment>> FindAppointmentsAsync(string status) =>
            await _unitOfWork.Repository<Appointment>().FindAsync(a => a.Status != null && a.Status.Contains(status));

        public async Task<int> CountAppointmentsAsync() =>
            await _unitOfWork.Repository<Appointment>().CountAsync();

        public async Task<List<Appointment>> GetAppointmentsByPatientAsync(int patientId)
        {
            return await _unitOfWork.Repository<Appointment>()
                .Query()
                .Include(a => a.Doctor)          // Include the doctor
                .ThenInclude(d => d.Department)  // Include the department of the doctor
                .Where(a => a.PatientId == patientId && !a.IsDeleted.GetValueOrDefault())
                .ToListAsync();
        }


        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(int doctorId) =>
            await _unitOfWork.Repository<Appointment>().FindAsync(a => a.DoctorId == doctorId);

        public async Task<int> GetPendingAppointmentsCountAsync()
        {
            var pendingAppointments = await _unitOfWork.Repository<Appointment>().FindAsync(a => a.Status == "Pending");
            return pendingAppointments.Count();
        }
        public async Task UpdateAppointmentStatusesAsync()
        {
            var appointments = await _unitOfWork.Repository<Appointment>()
                .FindAsync(a => a.Status == "Pending" || a.Status == "Active");

            foreach (var appointment in appointments)
            {
                if (appointment.AppointmentDateTime < DateTime.Now.AddHours(-24))
                {
                    appointment.Status = "Cancelled";
                    appointment.UpdatedDate = DateTime.UtcNow;
                    _unitOfWork.Repository<Appointment>().Update(appointment);
                }
                else if (appointment.AppointmentDateTime <= DateTime.Now)
                {
                    appointment.Status = "Active";
                    _unitOfWork.Repository<Appointment>().Update(appointment);
                }
            }
            await _unitOfWork.CompleteAsync();
        }
    }
}
