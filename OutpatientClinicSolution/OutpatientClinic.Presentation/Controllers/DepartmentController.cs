using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (!ModelState.IsValid) return View(department);

            await _departmentService.CreateDepartmentAsync(department);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            return department == null ? NotFound() : View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (!ModelState.IsValid) return View(department);

            // Ensure IsDeleted remains false when updating
            var existingDepartment = await _departmentService.GetDepartmentByIdAsync(department.DepartmentId);
            if (existingDepartment == null)
            {
                return NotFound();
            }

            existingDepartment.DepartmentName = department.DepartmentName;
            existingDepartment.UpdatedBy = "Admin"; // You can set this dynamically from the logged-in user
            existingDepartment.UpdatedDate = DateTime.Now;
            existingDepartment.IsDeleted ??= false; // Ensure IsDeleted is false if it was null

            await _departmentService.UpdateDepartmentAsync(existingDepartment);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
