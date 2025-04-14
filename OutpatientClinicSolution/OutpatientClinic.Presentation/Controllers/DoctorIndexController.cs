// Controllers/DoctorViewController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorIndexController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorIndexController(
            UserManager<ApplicationUser> userManager,
            IDoctorService doctorService,
            IAppointmentService appointmentService,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _doctorService = doctorService;
            _appointmentService = appointmentService;
            _unitOfWork = unitOfWork;
        }
        private async Task<bool> IsUserDoctor()
        {
            var user = await _userManager.GetUserAsync(User);
            return user != null && await _userManager.IsInRoleAsync(user, "Doctor");
        }
        public async Task<IActionResult> Index()
        {
            //var user = await _userManager.GetUserAsync(User);
            //var doctor = await _doctorService.GetDoctorByUserIdAsync(user.Id);

            //if (doctor == null)
            //{
            //    return NotFound("Doctor profile not found. Please contact support.");
            //}

            //ViewBag.DoctorId = doctor.DoctorId;
            return View();
        }

        // Medical Records (Redirect to MedicalRecordController)
        public IActionResult MedicalRecords() => RedirectToAction("Index", "MedicalRecord");

        // Prescriptions (Redirect to PrescriptionController)
        public IActionResult Prescriptions() => RedirectToAction("Index", "Prescription");

        // Lab Tests (Redirect to LabTestsController)
        public IActionResult LabTests() => RedirectToAction("Index", "LabTests");
        public IActionResult Medicine() => RedirectToAction("Index", "Medicine");

        // Filtered Appointments
        public async Task<IActionResult> Appointments(string filter)
        {
            var user = await _userManager.GetUserAsync(User);
            var doctor = await _doctorService.GetDoctorByUserIdAsync(user.Id);

            var appointments = await _appointmentService.GetAppointmentsByDoctorAsync(doctor.DoctorId);

            // Apply filters
            var now = DateTime.Now;
            appointments = filter switch
            {
                "previous" => appointments.Where(a => a.AppointmentDateTime < now.Date).ToList(),
                "current" => appointments.Where(a => a.AppointmentDateTime.Date == now.Date).ToList(),
                _ => appointments
            };

            ViewBag.Filter = filter;
            return View(appointments);
        }
    }
}