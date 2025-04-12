// Controllers/PatientViewController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using OutpatientClinic.Presentation.Models;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientViewController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IUnitOfWork _unitOfWork;

        public PatientViewController(
            UserManager<ApplicationUser> userManager,
            IPatientService patientService,
            IDoctorService doctorService,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _patientService = patientService;
            _doctorService = doctorService;
            _unitOfWork = unitOfWork;
        }

        // Dashboard
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var patient = await _patientService.GetPatientByUserIdAsync(user.Id);
            if (patient == null) return NotFound();

            // Fix for CS1501: No overload for method 'CountAsync' takes 1 arguments
            // The issue is likely that the `CountAsync` method requires a queryable source as its first argument.
            // Adding `.Query()` before calling `CountAsync` ensures the repository is queried properly.

            var labTestsCount = await _unitOfWork.Repository<LabTest>()
                .Query()
                .CountAsync(lt => lt.PatientId == patient.PatientId);

            var appointmentsCount = await _unitOfWork.Repository<Appointment>()
                .Query()
                .CountAsync(a => a.PatientId == patient.PatientId);

            var prescriptionsCount = await _unitOfWork.Repository<Prescription>()
                .Query()
                .CountAsync(p => p.Record.Appointment.PatientId == patient.PatientId);

            // Fetch primary doctor
            var doctor = patient.PrimaryDoctorId.HasValue
                ? await _doctorService.GetDoctorByIdAsync(patient.PrimaryDoctorId.Value)
                : null;

            var viewModel = new PatientDashboardViewModel
            {
                Patient = patient,
                User = user,
                LabTestsCount = labTestsCount,
                PrescriptionsCount = prescriptionsCount,
                AppointmentsCount = appointmentsCount,
                Doctor = doctor
            };

            return View(viewModel);
        }

        // Patient Details
        public async Task<IActionResult> Details()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var patient = await _patientService.GetPatientByUserIdAsync(user.Id);
            if (patient == null) return NotFound();

            return View(patient);
        }

        // Appointments List
        public async Task<IActionResult> Appointments()
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = await _patientService.GetPatientByUserIdAsync(user.Id);

            var appointments = await _unitOfWork.Repository<Appointment>()
                .Query()
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.DoctorNavigation)
                .Include(a => a.Department)
                .Include(a => a.Clinic)
                .Where(a => a.PatientId == patient.PatientId)
                .ToListAsync();

            return View(appointments);
        }

        // Lab Tests List
        public async Task<IActionResult> LabTests()
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = await _patientService.GetPatientByUserIdAsync(user.Id);

            var labTests = await _unitOfWork.Repository<LabTest>()
                .Query()
                .Include(lt => lt.Appointment)
                .Where(lt => lt.PatientId == patient.PatientId)
                .ToListAsync();

            return View(labTests);
        }

        // Prescriptions List
        public async Task<IActionResult> Prescriptions()
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = await _patientService.GetPatientByUserIdAsync(user.Id);

            var prescriptions = await _unitOfWork.Repository<Prescription>()
                .Query()
                .Include(p => p.Record)
                    .ThenInclude(r => r.Appointment)
                        .ThenInclude(a => a.Patient)
                .Where(p => p.Record != null &&
                            p.Record.Appointment != null &&
                            p.Record.Appointment.PatientId == patient.PatientId)
                .ToListAsync();

            return View(prescriptions);
        }
    }

    // ViewModel (Place in a separate file if preferred)
}