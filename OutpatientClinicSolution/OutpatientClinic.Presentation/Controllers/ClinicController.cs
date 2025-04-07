using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClinicController : Controller
    {
        private readonly IClinicService _clinicService;
        private readonly IDepartmentService _departmentService;

        public ClinicController(
            IClinicService clinicService,
            IDepartmentService departmentService)
        {
            _clinicService = clinicService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var clinics = await _clinicService.GetAllClinicsAsync();
            return View(clinics);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.DepartmentSuggestions = await _departmentService.GetAllDepartmentsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                await _clinicService.CreateClinicAsync(clinic);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DepartmentSuggestions = await _departmentService.GetAllDepartmentsAsync();
            return View(clinic);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var clinic = await _clinicService.GetClinicByIdAsync(id);
            if (clinic == null) return NotFound();

            ViewBag.DepartmentSuggestions = await _departmentService.GetAllDepartmentsAsync();
            return View(clinic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Clinic clinic)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _departmentService.GetAllDepartmentsAsync();
                return View(clinic);
            }

            var existingClinic = await _clinicService.GetClinicByIdAsync(clinic.ClinicId);
            if (existingClinic == null)
            {
                return NotFound();
            }

            existingClinic.ClinicName = clinic.ClinicName;
            existingClinic.Address = clinic.Address;
            existingClinic.Phone = clinic.Phone;
            existingClinic.UpdatedBy = "Admin"; // Set dynamically from logged-in user
            existingClinic.UpdatedDate = DateTime.Now;
            existingClinic.IsDeleted ??= false;

            await _clinicService.UpdateClinicAsync(existingClinic);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clinic = await _clinicService.GetClinicByIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clinicService.DeleteClinicAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}