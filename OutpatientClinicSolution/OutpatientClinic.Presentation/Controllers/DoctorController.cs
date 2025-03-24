using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IStaffService _staffService;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorController(IDoctorService doctorService, IStaffService staffService, IUnitOfWork unitOfWork)
        {
            _doctorService = doctorService;
            _staffService = staffService;
            _unitOfWork = unitOfWork;
        }

        // GET: /Doctor
        public async Task<IActionResult> Index(int? departmentId)
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            if (departmentId.HasValue)
            {
                doctors = doctors.Where(d => d.DepartmentId == departmentId.Value);
            }
            var departments = await _unitOfWork.Repository<Department>().GetAllAsync();
            ViewBag.Departments = departments;
            ViewBag.SelectedDepartment = departmentId;
            return View(doctors);
        }

        // GET: /Doctor/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        // GET: /Doctor/Create
        public async Task<IActionResult> Create()
        {
            // Populate dropdowns
            var departments = await _unitOfWork.Repository<Department>().GetAllAsync();
            ViewBag.Departments = departments;

            var staffList = await _staffService.GetAllStaffAsync();
            // Optionally filter out staff already assigned as doctors
            ViewBag.StaffList = staffList;
            return View();
        }

        // POST: /Doctor/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor, int staffId)
        {
            if (ModelState.IsValid)
            {
                var staff = await _staffService.GetStaffByIdAsync(staffId);
                if (staff == null)
                {
                    ModelState.AddModelError("", "Invalid staff selection.");
                }
                else
                {
                    // Set the doctor's primary key to the staff's ID.
                    doctor.DoctorId = staff.StaffId;
                    // Establish both sides of the relationship.
                    doctor.DoctorNavigation = staff;
                    staff.Doctor = doctor;
                    await _doctorService.CreateDoctorAsync(doctor);
                    return RedirectToAction(nameof(Index));
                }
            }

            // Repopulate dropdowns if there's a validation error.
            ViewBag.Departments = await _unitOfWork.Repository<Department>().GetAllAsync();
            ViewBag.StaffList = await _staffService.GetAllStaffAsync();
            return View(doctor);
        }

        // GET: /Doctor/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();

            ViewBag.Departments = await _unitOfWork.Repository<Department>().GetAllAsync();
            ViewBag.StaffList = await _staffService.GetAllStaffAsync();
            ViewBag.SelectedStaffId = doctor.DoctorNavigation?.StaffId;
            return View(doctor);
        }

        // POST: /Doctor/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Doctor doctor, int staffId)
        {
            if (id != doctor.DoctorId)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var staff = await _staffService.GetStaffByIdAsync(staffId);
                if (staff == null)
                {
                    ModelState.AddModelError("", "Invalid staff selection.");
                }
                else
                {
                    doctor.DoctorNavigation = staff;
                    // Ensure the primary key is still set correctly.
                    doctor.DoctorId = staff.StaffId;
                    var updated = await _doctorService.UpdateDoctorAsync(doctor);
                    if (updated)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", "Unable to update the doctor. Please try again.");
                }
            }
            ViewBag.Departments = await _unitOfWork.Repository<Department>().GetAllAsync();
            ViewBag.StaffList = await _staffService.GetAllStaffAsync();
            return View(doctor);
        }

        // GET: /Doctor/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        // POST: /Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _doctorService.DeleteDoctorAsync(id);
            if (!deleted)
                return BadRequest("Delete operation failed.");

            return RedirectToAction(nameof(Index));
        }
    }
}
