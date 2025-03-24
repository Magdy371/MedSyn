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
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IDepartmentService _departmentService;
        private readonly IStaffService _staffService;

        public DoctorController(
            IDoctorService doctorService,
            IDepartmentService departmentService,
            IStaffService staffService)
        {
            _doctorService = doctorService;
            _departmentService = departmentService;
            _staffService = staffService;
        }

        // GET: Doctor
        public async Task<IActionResult> Index(string searchString, string filterType, string filterValue)
        {
            var doctors = await _doctorService.GetAllDoctorsWithDetailsAsync() ?? new List<Doctor>();

            if (!string.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(d =>
                    d.DoctorNavigation != null &&
                    (d.DoctorNavigation.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    d.DoctorNavigation.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    d.Specialty.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    (d.LicenseNumber != null && d.LicenseNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase)))).ToList();
            }

            switch (filterType)
            {
                case "department":
                    if (!string.IsNullOrEmpty(filterValue) && int.TryParse(filterValue, out int departmentId))
                    {
                        doctors = doctors.Where(d => d.DepartmentId == departmentId).ToList();
                    }
                    break;
                case "specialty":
                    if (!string.IsNullOrEmpty(filterValue))
                    {
                        doctors = doctors.Where(d => d.Specialty == filterValue).ToList();
                    }
                    break;
            }

            var departments = await _departmentService.GetAllDepartmentsAsync() ?? new List<Department>();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");

            var specialties = doctors.Select(d => d.Specialty).Distinct().OrderBy(s => s).ToList();
            ViewBag.Specialties = new SelectList(specialties);

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentFilterType = filterType;
            ViewBag.CurrentFilterValue = filterValue;

            return View(doctors);
        }




        // GET: Doctor/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctor/Create
        public async Task<IActionResult> Create()
        {
            var allStaff = await _staffService.GetAllStaffAsync() ?? new List<Staff>();
            var existingDoctors = await _doctorService.GetAllDoctorsAsync() ?? new List<Doctor>();
            var existingDoctorIds = existingDoctors.Select(d => d.DoctorId).ToList();

            var availableStaff = allStaff
                .Where(s => !existingDoctorIds.Contains(s.StaffId))
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToList();

            var departments = await _departmentService.GetAllDepartmentsAsync() ?? new List<Department>();

            ViewBag.StaffId = new SelectList(availableStaff, "StaffId", "FullName");
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");

            return View(new DoctorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var doctor = new Doctor
                    {
                        DoctorId = model.StaffId,
                        DepartmentId = model.DepartmentId,
                        Specialty = model.Specialty,
                        LicenseNumber = model.LicenseNumber
                    };

                    await _doctorService.CreateDoctorAsync(doctor);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Unable to create doctor: {ex.Message}");
                }
            }

            var allStaff = await _staffService.GetAllStaffAsync() ?? new List<Staff>();
            var existingDoctors = await _doctorService.GetAllDoctorsAsync() ?? new List<Doctor>();
            var existingDoctorIds = existingDoctors.Select(d => d.DoctorId).ToList();

            var availableStaff = allStaff
                .Where(s => !existingDoctorIds.Contains(s.StaffId))
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToList();

            var departments = await _departmentService.GetAllDepartmentsAsync() ?? new List<Department>();

            ViewBag.StaffId = new SelectList(availableStaff, "StaffId", "FullName", model.StaffId);
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName", model.DepartmentId);

            return View(model);
        }


        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            var departments = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName", doctor.DepartmentId);

            var model = new DoctorViewModel
            {
                DoctorId = doctor.DoctorId,
                StaffId = doctor.DoctorId, // Same as DoctorId due to the 1:1 relationship
                DepartmentId = doctor.DepartmentId,
                Specialty = doctor.Specialty,
                LicenseNumber = doctor.LicenseNumber,
                StaffFirstName = doctor.DoctorNavigation.FirstName,
                StaffLastName = doctor.DoctorNavigation.LastName
            };

            return View(model);
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorViewModel model)
        {
            if (id != model.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var doctor = await _doctorService.GetDoctorByIdAsync(id);
                    if (doctor == null)
                    {
                        return NotFound();
                    }

                    // Update properties
                    doctor.DepartmentId = model.DepartmentId;
                    doctor.Specialty = model.Specialty;
                    doctor.LicenseNumber = model.LicenseNumber;

                    await _doctorService.UpdateDoctorAsync(doctor);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Unable to update doctor: {ex.Message}");
                }
            }

            // If we got this far, something failed, redisplay form
            var departments = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName", model.DepartmentId);

            return View(model);
        }

        // GET: Doctor/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _doctorService.DeleteDoctorAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Delete), new { id = id, error = ex.Message });
            }
        }
    }
}