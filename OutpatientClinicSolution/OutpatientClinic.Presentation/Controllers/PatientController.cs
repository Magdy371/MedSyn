using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using OutpatientClinic.Presentation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDoctorService _doctorService; // Assume implemented

        public PatientController(
            IPatientService patientService,
            UserManager<ApplicationUser> userManager,
            IDoctorService doctorService)
        {
            _patientService = patientService;
            _userManager = userManager;
            _doctorService = doctorService;
        }

        // GET: Patient/Index
        public async Task<IActionResult> Index()
        {
            // Get all users with the "Patient" role
            var patientsInRole = await _userManager.GetUsersInRoleAsync("Patient");
            var viewModels = new List<PatientViewModel>();

            foreach (var user in patientsInRole)
            {
                // Get the patient record (if available) using the user Id.
                var patientRecord = await _patientService.GetPatientByUserIdAsync(user.Id);

                viewModels.Add(new PatientViewModel
                {
                    PatientId = patientRecord?.PatientId,
                    UserId = user.Id,
                    FullName = user.FullName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    Dob = patientRecord?.Dob,
                    PrimaryDoctorId = patientRecord?.PrimaryDoctorId
                });
            }
            return View(viewModels);
        }

        // GET: Patient/Details?userId=...
        public async Task<IActionResult> Details(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var patientRecord = await _patientService.GetPatientByUserIdAsync(user.Id);
            var viewModel = new PatientViewModel
            {
                PatientId = patientRecord?.PatientId,
                UserId = user.Id,
                FullName = user.FullName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Dob = patientRecord?.Dob,
                PrimaryDoctorId = patientRecord?.PrimaryDoctorId
            };

            return View(viewModel);
        }

        // GET: Patient/Edit?userId=...
        public async Task<IActionResult> Edit(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var patientRecord = await _patientService.GetPatientByUserIdAsync(user.Id);
            var viewModel = new PatientViewModel
            {
                PatientId = patientRecord?.PatientId,
                UserId = user.Id,
                FullName = user.FullName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Dob = patientRecord?.Dob,
                PrimaryDoctorId = patientRecord?.PrimaryDoctorId
            };

            // Populate AvailableDoctors if a department is chosen
            viewModel.AvailableDoctors = await GetDoctorsSelectListAsync(viewModel.WantedDepartment);
            return View(viewModel);
        }

        // POST: Patient/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AvailableDoctors = await GetDoctorsSelectListAsync(viewModel.WantedDepartment);
                return View(viewModel);
            }

            var patientRecord = await _patientService.GetPatientByUserIdAsync(viewModel.UserId);
            if (patientRecord == null)
            {
                // Create new Patient record if it does not exist yet.
                var names = viewModel.FullName.Split(' ');
                patientRecord = new Patient
                {
                    UserId = viewModel.UserId,
                    FirstName = names.FirstOrDefault() ?? string.Empty,
                    LastName = names.Length > 1 ? names[1] : string.Empty,
                    Dob = viewModel.Dob ?? DateOnly.MinValue,
                    PrimaryDoctorId = viewModel.PrimaryDoctorId
                };
                await _patientService.CreatePatientAsync(patientRecord);
            }
            else
            {
                // Update existing record
                patientRecord.Dob = viewModel.Dob ?? patientRecord.Dob;
                patientRecord.PrimaryDoctorId = viewModel.PrimaryDoctorId;
                var names = viewModel.FullName.Split(' ');
                patientRecord.FirstName = names.FirstOrDefault() ?? patientRecord.FirstName;
                patientRecord.LastName = names.Length > 1 ? names[1] : patientRecord.LastName;
                await _patientService.UpdatePatientAsync(patientRecord);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Patient/Delete?userId=...
        public async Task<IActionResult> Delete(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var patientRecord = await _patientService.GetPatientByUserIdAsync(user.Id);
            var viewModel = new PatientViewModel
            {
                PatientId = patientRecord?.PatientId,
                UserId = user.Id,
                FullName = user.FullName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Dob = patientRecord?.Dob,
                PrimaryDoctorId = patientRecord?.PrimaryDoctorId
            };

            return View(viewModel);
        }

        // POST: Patient/Delete?userId=...
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return NotFound();

            var patientRecord = await _patientService.GetPatientByUserIdAsync(userId);
            if (patientRecord != null)
            {
                await _patientService.DeletePatientAsync(patientRecord.PatientId);
            }

            return RedirectToAction(nameof(Index));
        }

        // Helper to build a doctor list for a given department
        private async Task<IEnumerable<SelectListItem>> GetDoctorsSelectListAsync(string? department)
        {
            var doctors = await _doctorService.GetDoctorsByDepartmentAsync(department);
            return doctors.Select(d => new SelectListItem
            {
                Value = d.DoctorId.ToString(),
                Text = d.DoctorNavigation.FullName // Use the FullName from the related Staff entity
            });
        }

    }
}
