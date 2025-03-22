using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IContactInfoService _contactInfoService;

        public StaffController(IStaffService staffService, IContactInfoService contactInfoService)
        {
            _staffService = staffService;
            _contactInfoService = contactInfoService;
        }

        public async Task<IActionResult> Index()
        {
            var staffList = await _staffService.GetAllStaffAsync();
            return View(staffList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Staff staff)
        {
            if (!ModelState.IsValid) return View(staff);

            staff.CreatedBy = "Admin"; // Can be dynamically set based on logged-in user
            staff.CreatedDate = DateTime.Now;
            staff.IsDeleted = false; // Ensure it's not marked as deleted

            await _staffService.CreateStaffAsync(staff);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Staff staff)
        {
            if (!ModelState.IsValid) return View(staff);

            staff.UpdatedBy = "Admin"; // Set dynamically from logged-in user
            staff.UpdatedDate = DateTime.Now;

            await _staffService.UpdateStaffAsync(staff);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _staffService.DeleteStaffAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
