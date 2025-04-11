using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using OutpatientClinic.Presentation.Models;
using System;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize]
    public class LabTestsController : Controller
    {
        private readonly ILabTestService _labTestService;
        private readonly IPatientService _patientService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LabTestsController(
            ILabTestService labTestService,
            IPatientService patientService,
            UserManager<ApplicationUser> userManager)
        {
            _labTestService = labTestService;
            _patientService = patientService;
            _userManager = userManager;
        }

        // GET: LabTests
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public async Task<IActionResult> Index()
        {
            var labTests = await _labTestService.GetAllLabTestsAsync();
            var viewModel = labTests.Select(lt => new LabTestIndexViewModel
            {
                TestId = lt.TestId,
                TestName = lt.TestName,
                TestDate = lt.TestDate,
                PatientName = lt.Patient != null
        ? $"{lt.Patient.FirstName} {lt.Patient.LastName}"
        : "Unknown", 
                AppointmentId = lt.AppointmentId,
                CreatedBy = lt.CreatedBy ?? "Unknown"
            }).ToList();

            return View(viewModel);
        }

        // GET: LabTests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the lab test with Patient and Appointment included
            var labTest = await _labTestService.GetLabTestByIdAsync(id.Value);

            if (labTest == null || (labTest.IsDeleted ?? false))
            {
                return NotFound();
            }

            // Security check for patients (restrict access to their own records)
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(user, "Patient"))
            {
                var patient = await _patientService.GetPatientByUserIdAsync(user.Id);
                if (patient == null || labTest.PatientId != patient.PatientId)
                {
                    return Forbid();
                }
            }

            return View(labTest);
        }

        // GET: LabTests/Create
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Create()
        {
            await PopulatePatientsDropdown();
            return View(new LabTest { TestDate = DateTime.Now });
        }

        // POST: LabTests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Create([Bind("TestName,TestDate,PatientId,Results")] LabTest labTest)
        {
            if (ModelState.IsValid)
            {
                var errors = ModelState
                  .Where(e => e.Value.Errors.Count > 0)
                  .Select(e => new { e.Key, e.Value.Errors })
                  .ToList();
                var patient = await _patientService.GetPatientByIdAsync(labTest.PatientId);
                if (patient == null || (patient.IsDeleted ?? false))
                {
                    ModelState.AddModelError("PatientId", "Selected patient does not exist.");
                }
                else
                {
                    labTest.CreatedBy = User.Identity?.Name ?? "System";
                    labTest.CreatedDate = DateTime.Now;
                    labTest.IsDeleted = false;

                    await _labTestService.CreateLabTestAsync(labTest);
                    return RedirectToAction(nameof(Index));
                }
            }

            await PopulatePatientsDropdown();
            return View(labTest);
        }

        // GET: LabTests/Edit/5
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var labTest = await _labTestService.GetLabTestByIdAsync(id.Value);
            if (labTest == null || (labTest.IsDeleted ?? false)) return NotFound();

            await PopulatePatientsDropdown();
            return View(labTest);
        }

        // POST: LabTests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Edit(int id, [Bind("TestId,TestName,TestDate,PatientId,Results,AppointmentId")] LabTest labTest)
        {
            if (id != labTest.TestId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    labTest.UpdatedBy = User.Identity?.Name ?? "System";
                    labTest.UpdatedDate = DateTime.Now;
                    await _labTestService.UpdateLabTestAsync(labTest);
                }
                catch (Exception)
                {
                    if (!await LabTestExists(labTest.TestId))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            await PopulatePatientsDropdown();
            return View(labTest);
        }

        // GET: LabTests/Delete/5
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var labTest = await _labTestService.GetLabTestByIdAsync(id.Value);
            if (labTest == null || (labTest.IsDeleted ?? false)) return NotFound();

            return View(labTest);
        }

        // POST: LabTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labTest = await _labTestService.GetLabTestByIdAsync(id);
            if (labTest != null)
            {
                labTest.IsDeleted = true;
                labTest.UpdatedBy = User.Identity?.Name ?? "System";
                labTest.UpdatedDate = DateTime.Now;
                await _labTestService.UpdateLabTestAsync(labTest);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LabTestExists(int id)
        {
            var labTest = await _labTestService.GetLabTestByIdAsync(id);
            return labTest != null;
        }

        private async Task PopulatePatientsDropdown()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            var activePatients = patients
                .Where(p => !(p.IsDeleted ?? false)) // Explicit check for soft-delete
                .Select(p => new
                {
                    p.PatientId,
                    Text = $"{p.FirstName} {p.LastName}"
                })
                .ToList();

            ViewData["Patients"] = new SelectList(activePatients, "PatientId", "Text");
        }
    }
}