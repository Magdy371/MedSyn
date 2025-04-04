﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync() =>
            await _unitOfWork.Repository<Appointment>().GetAllAsync();

        public async Task<Appointment> GetAppointmentByIdAsync(int id) =>
            await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
            await _unitOfWork.CompleteAsync();
            return appointment;
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

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(int patientId) =>
            await _unitOfWork.Repository<Appointment>().FindAsync(a => a.PatientId == patientId);

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(int doctorId) =>
            await _unitOfWork.Repository<Appointment>().FindAsync(a => a.DoctorId == doctorId);

        public async Task<int> GetPendingAppointmentsCountAsync()
        {
            var pendingAppointments = await _unitOfWork.Repository<Appointment>().FindAsync(a => a.Status == "Pending");
            return pendingAppointments.Count();
        }

    }
}
