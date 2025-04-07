using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using OutpatientClinic.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Web.Controllers
{
    [Authorize(Roles = "Receptionist,Doctor,Admin")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IClinicService _clinicService;
        private readonly IDoctorService _doctorService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminService _adminService;

        public AppointmentController(
            IAppointmentService appointmentService,
            IPatientService patientService,
            IClinicService clinicService,
            IDoctorService doctorService,
            UserManager<ApplicationUser> userManager,
            IAdminService adminService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _clinicService = clinicService;
            _doctorService = doctorService;
            _userManager = userManager;
            _adminService = adminService;
        }

        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        // GET: Appointment/Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            var viewModel = new AppointmentCreateViewModel();
            return View(viewModel);
        }

        // Patient search endpoint
        [HttpGet]
        public async Task<IActionResult> SearchPatients(string term)
        {
            var patients = await _patientService.SearchPatientsAsync(term);
            return Json(patients.Select(p => new {
                id = p.PatientId,
                label = $"{p.FullName} ({p.Email})",
                email = p.Email
            }));
        }

        // POST: Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentCreateViewModel model)
        {
            using var transaction = await _appointmentService.BeginTransactionAsync();
            try
            {
                if (ModelState.IsValid)
                {
                    Patient patient = null;

                    // Existing Patient Flow
                    if (model.SelectedPatientId.HasValue)
                    {
                        // Retrieve existing patient
                        patient = await _patientService.GetPatientByIdAsync(model.SelectedPatientId.Value);

                        // If a doctor is selected, perform department validation
                        if (model.Appointment.DoctorId != 0)
                        {
                            var doctor = await _doctorService.GetDoctorById(model.Appointment.DoctorId);
                            var existingAppointments = await _appointmentService.GetAppointmentsByPatientAsync(patient.PatientId);

                            var doctorDeptId = doctor?.Department?.DepartmentId;
                            if (doctorDeptId != null && existingAppointments.Any(a =>
                                    a.Doctor?.Department?.DepartmentId == doctorDeptId))
                            {
                                return RedirectToAction("ExistingAppointments", new { patientId = patient.PatientId });
                            }
                        }
                    }
                    // New Patient Flow
                    else
                    {
                        // Create new user
                        var newUser = new ApplicationUser
                        {
                            Email = model.NewPatientEmail,
                            UserName = model.NewPatientEmail,
                            FullName = $"{model.NewPatientFirstName} {model.NewPatientLastName}"
                        };

                        var result = await _userManager.CreateAsync(newUser, "TempPassword123!");
                        if (!result.Succeeded)
                            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

                        await _adminService.AssignRoleAsync(newUser.Id, "Patient");

                        // Create patient record
                        patient = new Patient
                        {
                            UserId = newUser.Id,
                            FirstName = model.NewPatientFirstName,
                            LastName = model.NewPatientLastName,
                            Dob = model.NewPatientDob
                        };

                        await _patientService.CreatePatientAsync(patient);
                    }

                    // Create appointment
                    model.Appointment.PatientId = patient.PatientId;
                    model.Appointment.ClinicId = model.ClinicId ?? (await _clinicService.GetEmergencyClinicAsync()).ClinicId;

                    await _appointmentService.CreateAppointmentAsync(model.Appointment);
                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }

                await LoadDropdownsAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError("", $"Error creating appointment: {ex.Message}");
                await LoadDropdownsAsync();
                return View(model);
            }
        }

        // Existing appointments view
        public async Task<IActionResult> ExistingAppointments(int patientId)
        {
            var appointments = await _appointmentService.GetAppointmentsByPatientAsync(patientId);
            return View(appointments);
        }

        // Helper method to load dropdown lists
        private async Task LoadDropdownsAsync()
        {
            var clinics = await _clinicService.GetAllClinicsAsync();
            var doctors = await _doctorService.GetAllDoctorsAsync();

            ViewBag.Clinics = clinics;

            ViewBag.Doctors = doctors.Select(d => new
            {
                DoctorId = d.DoctorId,
                FullName = d.DoctorNavigation != null
                    ? $"{d.DoctorNavigation.FirstName} {d.DoctorNavigation.LastName}"
                    : $"Doctor #{d.DoctorId}"
            }).ToList();
        }
    }
}
