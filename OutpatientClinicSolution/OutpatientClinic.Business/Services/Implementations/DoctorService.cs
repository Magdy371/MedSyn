using Microsoft.EntityFrameworkCore;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    public DoctorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync() =>
        await _unitOfWork.Repository<Doctor>().GetAllAsync();

    public async Task<Doctor?> GetDoctorByIdAsync(int id)
    {
        return await _unitOfWork.Repository<Doctor>()
            .Query()
            .Include(d => d.DoctorNavigation)
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.DoctorId == id);
    }

    public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
    {
        await _unitOfWork.Repository<Doctor>().AddAsync(doctor);
        var result = await _unitOfWork.CompleteAsync();
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

    public async Task<Doctor?> GetDoctorByLicenseNumberAsync(string licenseNumber)
    {
        var doctors = await _unitOfWork.Repository<Doctor>().FindAsync(d => d.LicenseNumber == licenseNumber);
        return doctors.FirstOrDefault() ?? throw new KeyNotFoundException($"Doctor with license number '{licenseNumber}' not found.");
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctorsWithDetailsAsync()
    {
        return await _unitOfWork.Repository<Doctor>()
            .Query()
            .Include(d => d.DoctorNavigation)
            .Include(d => d.Department)
            .ToListAsync();
    }

    // NEW: Implementation of GetDoctorsByDepartmentAsync
    //public async Task<IEnumerable<Doctor>> GetDoctorsByDepartmentAsync(string? department)
    //{
    //    var query = _unitOfWork.Repository<Doctor>()
    //        .Query()
    //        .Include(d => d.Department)
    //        .AsQueryable();

    //    if (!string.IsNullOrWhiteSpace(department))
    //    {
    //        query = query.Where(d => d.Department.DepartmentName.Contains(department));
    //    }
    //    return await query.ToListAsync();
    //}
    public async Task<IEnumerable<Doctor>> GetDoctorsByDepartmentAsync(string? department)
    {
        var query = _unitOfWork.Repository<Doctor>()
            .Query()
            .Include(d => d.Department)
            .Include(d => d.DoctorNavigation) // Add this to include the Staff navigation property
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(department))
        {
            query = query.Where(d => d.Department.DepartmentName.Contains(department));
        }
        return await query.ToListAsync();
    }
}
