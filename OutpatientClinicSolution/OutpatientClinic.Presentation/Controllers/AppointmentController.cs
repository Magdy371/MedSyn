using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.DataAccess.Context;
using OutpatientClinic.DataAccess.Entities;
using OutpatientClinic.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OutpatientClinic.Web.Controllers
{
    [Authorize(Roles = S_R)]
    public class AppointmentController : Controller
    {
        private const string S_R = "Admin,Doctor,Receptionist";
        private readonly OutpatientClinicDbContext _context;

        public AppointmentController(OutpatientClinicDbContext context)
        {
            _context = context;
        }

        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.DoctorNavigation)
                .Include(a => a.Clinic)
                .Include(a => a.Department)
                .Where(a => a.IsDeleted != true)
                .OrderByDescending(a => a.AppointmentDateTime)
                .ToListAsync();

            return View(appointments);
        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Clinic)
                .Include(a => a.Department)
                .Include(a => a.MedicalRecord)
                .FirstOrDefaultAsync(m => m.AppointmentId == id && m.IsDeleted != true);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointment/Create
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new AppointmentCreateViewModel());
        }

        // POST: Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public async Task<IActionResult> Create(AppointmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    PatientId = model.PatientId,
                    DepartmentId = model.DepartmentId,
                    ClinicId = model.ClinicId,
                    DoctorId = model.DoctorId, // Added DoctorId
                    AppointmentDateTime = model.AppointmentDateTime,
                    Notes = model.Notes,
                    CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Status = "Scheduled"
                };

                try
                {
                    _context.Add(appointment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save. Try again, and if the problem persists, contact your system administrator.");
                }
            }

            await PopulateDropdowns(); // Adjust as needed for view model
            return View(model);
        }




        // GET: Appointment/Edit/5
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id && a.IsDeleted != true);

            if (appointment == null)
            {
                return NotFound();
            }

            await PopulateDropdowns(appointment);
            return View(appointment);
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,PatientId,DoctorId,DepartmentId,ClinicId,AppointmentDateTime,Status,Notes,CreatedBy,CreatedDate")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appointment.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    appointment.UpdatedDate = DateTime.Now;

                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdowns(appointment);
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Clinic)
                .Include(a => a.Department)
                .FirstOrDefaultAsync(m => m.AppointmentId == id && m.IsDeleted != true);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment != null)
            {
                // Soft delete
                appointment.IsDeleted = true;
                appointment.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                appointment.UpdatedDate = DateTime.Now;

                _context.Update(appointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Appointment/PatientAppointments
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> PatientAppointments()
        {
            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the patient associated with this user
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
            {
                return NotFound("Patient profile not found");
            }

            // Get this patient's appointments
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Clinic)
                .Include(a => a.Department)
                .Where(a => a.PatientId == patient.PatientId && a.IsDeleted != true)
                .OrderByDescending(a => a.AppointmentDateTime)
                .ToListAsync();

            return View("Index", appointments);
        }

        private async Task PopulateDropdowns(Appointment appointment = null)
        {
            // Patients (ensure IsDeleted filter is applied)
            var patients = await _context.Patients
                .Where(p => p.IsDeleted != true)
                .Select(p => new { p.PatientId, FullName = $"{p.FirstName} {p.LastName}" })
                .ToListAsync();
            ViewData["PatientId"] = new SelectList(patients, "PatientId", "FullName", appointment?.PatientId);

            // Departments (active only)
            var departments = await _context.Departments
                .Where(d => d.IsDeleted != true)
                .ToListAsync();
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "DepartmentName", appointment?.DepartmentId);

            // Clinics (active only)
            var clinics = await _context.Clinics
                .Where(c => c.IsDeleted != true)
                .ToListAsync();
            ViewData["ClinicId"] = new SelectList(clinics, "ClinicId", "ClinicName", appointment?.ClinicId);

            // Doctors (active staff only)
            var doctors = new List<object>();
            if (appointment != null && appointment.DepartmentId > 0)
            {
                doctors = await _context.Doctors
                    .Include(d => d.DoctorNavigation)
                    .Where(d => d.DepartmentId == appointment.DepartmentId && d.DoctorNavigation.IsDeleted != true)
                    .Select(d => new {
                        d.DoctorId,
                        FullName = $"{d.DoctorNavigation.FirstName} {d.DoctorNavigation.LastName} ({d.Specialty})"
                    })
                    .ToListAsync<object>();
            }

            // Add "Not Assigned" as the first option
            doctors.Insert(0, new { DoctorId = (int?)null, FullName = "-- Not Assigned --" });

            ViewData["DoctorId"] = new SelectList(doctors, "DoctorId", "FullName", appointment?.DoctorId);
            // Status (default to "Scheduled")
            ViewData["Status"] = new SelectList(new List<string> { "Scheduled" });
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
        // GET: Appointment/GetDoctorsByDepartment
        [HttpGet]
        public async Task<JsonResult> GetDoctorsByDepartment(int departmentId)
        {
            var doctors = await _context.Doctors
                .Include(d => d.DoctorNavigation)
                .Where(d => d.DepartmentId == departmentId && d.DoctorNavigation.IsDeleted != true)
                .Select(d => new {
                    value = d.DoctorId,
                    text = $"{d.DoctorNavigation.FirstName} {d.DoctorNavigation.LastName} ({d.Specialty})"
                })
                .ToListAsync();

            return Json(doctors);
        }
    }
}