// AppointmentController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using OutpatientClinic.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IDepartmentService _departmentService;
        private readonly IClinicService _clinicService;

        public AppointmentController(
            IAppointmentService appointmentService,
            IPatientService patientService,
            IDoctorService doctorService,
            IDepartmentService departmentService,
            IClinicService clinicService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
            _departmentService = departmentService;
            _clinicService = clinicService;
        }

        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentService.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // GET: Appointment/Create
        public async Task<IActionResult> Create()
        {
            await PopulateViewData();
            return View(new AppointmentViewModel());
        }

        // POST: Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    PatientId = model.PatientId,
                    DoctorId = model.DoctorId,
                    DepartmentId = model.DepartmentId,
                    ClinicId = model.ClinicId ?? await GetEmergencyClinicId(),
                    AppointmentDateTime = model.AppointmentDateTime,
                    Status = "Pending",
                    Notes = model.Notes,
                    CreatedDate = DateTime.UtcNow
                };

                await _appointmentService.CreateAppointmentAsync(appointment);
                return RedirectToAction(nameof(Index));
            }
            await PopulateViewData();
            return View(model);
        }

        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _appointmentService.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var model = new AppointmentViewModel
            {
                AppointmentId = appointment.AppointmentId,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                DepartmentId = appointment.DepartmentId,
                ClinicId = appointment.ClinicId,
                AppointmentDateTime = appointment.AppointmentDateTime,
                Notes = appointment.Notes,
                Status = appointment.Status
            };

            await PopulateViewData(appointment.DepartmentId, appointment.DoctorId);
            return View(model);
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentViewModel model)
        {
            if (id != model.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var appointment = await _appointmentService.GetAppointmentById(id);
                if (appointment == null)
                {
                    return NotFound();
                }

                appointment.PatientId = model.PatientId;
                appointment.DoctorId = model.DoctorId;
                appointment.DepartmentId = model.DepartmentId;
                appointment.ClinicId = model.ClinicId ?? await GetEmergencyClinicId();
                appointment.AppointmentDateTime = model.AppointmentDateTime;
                appointment.Notes = model.Notes;
                appointment.Status = model.Status;
                appointment.UpdatedDate = DateTime.UtcNow;

                await _appointmentService.UpdateAppointmentAsync(appointment);
                return RedirectToAction(nameof(Index));
            }
            await PopulateViewData(model.DepartmentId, model.DoctorId);
            return View(model);
        }

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentService.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> GetDoctorsByDepartment(int departmentId)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(departmentId);
            var doctors = await _doctorService.GetDoctorsByDepartmentAsync(department?.DepartmentName);
            return Json(doctors.Select(d => new { d.DoctorId, Name = $"{d.DoctorNavigation?.FirstName} {d.DoctorNavigation?.LastName}" }));
        }

        [HttpGet]
        public async Task<JsonResult> SearchPatients(string term)
        {
            var results = await _patientService.SearchPatientsAsync(term);
            return Json(results);
        }

        private async Task PopulateViewData(int? departmentId = null, int? doctorId = null)
        {
            ViewBag.Departments = new SelectList(
                await _departmentService.GetAllDepartmentsAsync(),
                "DepartmentId",
                "DepartmentName",
                departmentId);

            ViewBag.Clinics = new SelectList(
                await _clinicService.GetAllClinicsAsync(),
                "ClinicId",
                "ClinicName");

            if (departmentId.HasValue)
            {
                var department = await _departmentService.GetDepartmentByIdAsync(departmentId.Value);
                var doctors = await _doctorService.GetDoctorsByDepartmentAsync(department?.DepartmentName);
                ViewBag.Doctors = new SelectList(doctors, "DoctorId", "DoctorNavigation.FullName", doctorId);
            }
            else
            {
                ViewBag.Doctors = new SelectList(new List<Doctor>());
            }
        }

        private async Task<int> GetEmergencyClinicId()
        {
            var emergencyClinic = await _clinicService.GetEmergencyClinicAsync();
            return emergencyClinic.ClinicId;
        }
    }

    
}