﻿using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task<Doctor> CreateDoctorAsync(Doctor doctor);
        Task<bool> UpdateDoctorAsync(Doctor doctor);
        Task<bool> DeleteDoctorAsync(int id);
        Task<IEnumerable<Doctor>> FindDoctorsAsync(string specialty);
        Task<int> CountDoctorsAsync();
        Task<Doctor?> GetDoctorByLicenseNumberAsync(string licenseNumber);
    }
}
