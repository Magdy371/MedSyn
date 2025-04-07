using Microsoft.EntityFrameworkCore.Storage;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<bool> UpdateAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentStatusesAsync();
        Task<bool> DeleteAppointmentAsync(int id);
        Task<IEnumerable<Appointment>> FindAppointmentsAsync(string status);
        Task<int> CountAppointmentsAsync();
        Task<List<Appointment>> GetAppointmentsByPatientAsync(int patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(int doctorId);
    }
}
