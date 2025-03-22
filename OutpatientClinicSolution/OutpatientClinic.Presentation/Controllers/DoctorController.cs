using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IDepartmentService _departmentService;
        private readonly IStaffService _staffService;

        public DoctorController(IDoctorService doctorService, IDepartmentService departmentService, IStaffService staffService)
        {
            _doctorService = doctorService;
            _departmentService = departmentService;
            _staffService = staffService;
        }

        public async Task<IActionResult> Index(int? departmentId, string? specialty)
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();

            foreach (var doctor in doctors)
            {
                doctor.DoctorNavigation = await _staffService.GetStaffByIdAsync(doctor.DoctorId);
                doctor.Department = await _departmentService.GetDepartmentByIdAsync(doctor.DepartmentId);
            }

            if (departmentId.HasValue)
                doctors = doctors.Where(d => d.DepartmentId == departmentId.Value).ToList();

            if (!string.IsNullOrEmpty(specialty))
                doctors = doctors.Where(d => d.Specialty.Contains(specialty)).ToList();

            ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName");
            return View(doctors);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName");
                return View(doctor);
            }

            // Step 1: Create Staff First
            var staff = new Staff
            {
                FirstName = doctor.DoctorNavigation?.FirstName ?? "",
                LastName = doctor.DoctorNavigation?.LastName ?? "",
                CreatedBy = "Admin", // Can be dynamically set
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            await _staffService.CreateStaffAsync(staff);

            // Step 2: Assign Staff ID as Doctor ID
            doctor.DoctorId = staff.StaffId;

            // Step 3: Save Doctor
            await _doctorService.CreateDoctorAsync(doctor);

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();

            doctor.DoctorNavigation = await _staffService.GetStaffByIdAsync(doctor.DoctorId);
            ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName");

            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName");
                return View(doctor);
            }

            // Step 1: Update Staff First
            var staff = await _staffService.GetStaffByIdAsync(doctor.DoctorId);
            if (staff != null)
            {
                staff.FirstName = doctor.DoctorNavigation?.FirstName ?? "";
                staff.LastName = doctor.DoctorNavigation?.LastName ?? "";
                staff.UpdatedBy = "Admin";
                staff.UpdatedDate = DateTime.Now;
                await _staffService.UpdateStaffAsync(staff);
            }

            // Step 2: Update Doctor
            await _doctorService.UpdateDoctorAsync(doctor);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();

            doctor.DoctorNavigation = await _staffService.GetStaffByIdAsync(doctor.DoctorId);
            doctor.Department = await _departmentService.GetDepartmentByIdAsync(doctor.DepartmentId);
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
