using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using OutpatientClinic.Presentation.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IClinicService _clinicService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBackgroundTaskQueue _taskQueue;

        public AppointmentController(
            IAppointmentService appointmentService,
            IPatientService patientService,
            IDoctorService doctorService,
            IClinicService clinicService,
            UserManager<ApplicationUser> userManager,
            IBackgroundTaskQueue taskQueue)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
            _clinicService = clinicService;
            _userManager = userManager;
            _taskQueue = taskQueue;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new AppointmentViewModel
            {
                AppointmentDateTime = DateTime.Now.AddHours(1)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await PopulateDropdowns();
                    return View(model);
                }

                ApplicationUser? user = null;
                Patient? patient = null;

                // Existing patient flow
                if (!string.IsNullOrEmpty(model.UserId))
                {
                    user = await _userManager.FindByIdAsync(model.UserId);
                    if (user != null)
                    {
                        patient = await _patientService.GetPatientByUserIdAsync(model.UserId);
                    }
                }

                // New patient registration
                if (patient == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FullName = model.PatientName
                    };

                    var createResult = await _userManager.CreateAsync(user, "TempPass123!");
                    if (!createResult.Succeeded)
                    {
                        AddIdentityErrors(createResult);
                        await PopulateDropdowns();
                        return View(model);
                    }

                    await _userManager.AddToRoleAsync(user, "Patient");

                    patient = new Patient
                    {
                        UserId = user.Id,
                        FirstName = model.PatientName.Split(' ')[0],
                        LastName = model.PatientName.Split(' ')[1],
                        Dob = DateOnly.FromDateTime(DateTime.Now)
                    };
                    await _patientService.CreatePatientAsync(patient);
                }

                // Clinic and doctor assignment
                var (clinic, doctor) = await DetermineClinicAndDoctor(patient.PatientId, model.ClinicId);

                if (clinic == null || doctor == null)
                {
                    TempData["Error"] = "Could not find suitable clinic/doctor assignment";
                    return RedirectToAction(nameof(Index));
                }

                var appointment = new Appointment
                {
                    PatientId = patient.PatientId,
                    DoctorId = doctor.DoctorId,
                    ClinicId = clinic.ClinicId,
                    AppointmentDateTime = model.AppointmentDateTime,
                    Status = "Pending",
                    CreatedDate = DateTime.UtcNow
                };

                await _appointmentService.CreateAppointmentAsync(appointment);

                // Queue background status check
                _taskQueue.QueueBackgroundWorkItem(async token =>
                {
                    await _appointmentService.UpdateAppointmentStatusesAsync();
                });

                TempData["Success"] = "Appointment created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error creating appointment: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchPatients(string term)
        {
            var patients = await _patientService.FindPatientsAsync(term);
            return Json(patients.Select(p => new
            {
                id = p.UserId,
                name = $"{p.FirstName} {p.LastName}",
                email = p.Contact?.Email
            }));
        }

        private async Task<(Clinic clinic, Doctor doctor)> DetermineClinicAndDoctor(int patientId, int? clinicId)
        {
            var existingAppointments = await _appointmentService.GetAppointmentsByPatientAsync(patientId);
            Clinic? clinic = null;
            Doctor? doctor = null;

            if (existingAppointments.Any())
            {
                var lastAppointment = existingAppointments.MaxBy(a => a.AppointmentDateTime);
                var department = (await _doctorService.GetDoctorByIdAsync(lastAppointment.DoctorId))?.Department;

                clinic = await _clinicService.GetClinicByIdAsync(lastAppointment.ClinicId);
                doctor = (await _doctorService.GetDoctorsByDepartmentAsync(department?.DepartmentName))
                    .FirstOrDefault();
            }
            else
            {
                clinic = clinicId.HasValue
                    ? await _clinicService.GetClinicByIdAsync(clinicId.Value)
                    : await _clinicService.GetClinicByNameAsync("Emergency");

                doctor = (await _doctorService.GetDoctorsByDepartmentAsync("Emergency"))
                    .FirstOrDefault();

                if (doctor == null)
                {
                    doctor = (await _doctorService.GetAllDoctorsAsync()).FirstOrDefault();
                }
            }

            return (clinic, doctor);
        }

        private async Task PopulateDropdowns()
        {
            ViewBag.Clinics = await _clinicService.GetAllClinicAsync();
            ViewBag.Doctors = await _doctorService.GetAllDoctorsWithDetailsAsync();
        }

        private void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}