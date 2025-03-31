using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using OutpatientClinic.Presentation.Models;

[Authorize(Roles = "Admin")]
public class DoctorController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IDoctorService _doctorService;
    private readonly IDepartmentService _departmentService;

    public DoctorController(
        UserManager<ApplicationUser> userManager,
        IDoctorService doctorService,
        IDepartmentService departmentService)
    {
        _userManager = userManager;
        _doctorService = doctorService;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index(string licenseNumber, string department)
    {
        var doctorUsers = await _userManager.GetUsersInRoleAsync("Doctor");
        var doctorViewModels = new List<DoctorViewModel>();

        foreach (var user in doctorUsers)
        {
            var doctor = await _doctorService.GetDoctorByUserIdAsync(user.Id);
            doctorViewModels.Add(new DoctorViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                LicenseNumber = doctor?.LicenseNumber ?? "Not set",
                Specialty = doctor?.Specialty ?? "Not set",
                DepartmentName = doctor?.Department?.DepartmentName ?? "Not assigned"
            });
        }

        // Apply filters
        if (!string.IsNullOrEmpty(licenseNumber))
        {
            doctorViewModels = doctorViewModels
                .Where(d => d.LicenseNumber.Contains(licenseNumber, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (!string.IsNullOrEmpty(department))
        {
            doctorViewModels = doctorViewModels
                .Where(d => d.DepartmentName.Contains(department, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Populate departments for filter dropdown
        var departments = await _departmentService.GetAllDepartmentsAsync();
        ViewBag.Departments = new SelectList(departments, "DepartmentName", "DepartmentName");

        return View(doctorViewModels);
    }

    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
        var departments = await _departmentService.GetAllDepartmentsAsync();

        var model = new DoctorEditViewModel
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            LicenseNumber = doctor?.LicenseNumber ?? "",
            Specialty = doctor?.Specialty ?? "",
            DepartmentId = doctor?.DepartmentId ?? 0,
            Departments = new SelectList(departments, "DepartmentId", "DepartmentName", doctor?.DepartmentId)
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(DoctorEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName", model.DepartmentId);
            return View(model);
        }

        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null) return NotFound();

        // Update user details
        user.FullName = model.FullName;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        await _userManager.UpdateAsync(user);

        // Update or create doctor details
        var doctor = await _doctorService.GetDoctorByUserIdAsync(model.UserId);
        if (doctor == null)
        {
            // Split FullName into FirstName and LastName for Staff
            var fullNameParts = user.FullName.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            var firstName = fullNameParts.Length > 0 ? fullNameParts[0] : "Unknown";
            var lastName = fullNameParts.Length > 1 ? fullNameParts[1] : "Unknown";

            doctor = new Doctor
            {
                DoctorNavigation = new Staff
                {
                    UserId = model.UserId,
                    FirstName = firstName,
                    LastName = lastName
                },
                Specialty = model.Specialty,
                LicenseNumber = model.LicenseNumber,
                DepartmentId = model.DepartmentId
            };
            await _doctorService.CreateDoctorAsync(doctor);
        }
        else
        {
            doctor.Specialty = model.Specialty;
            doctor.LicenseNumber = model.LicenseNumber;
            doctor.DepartmentId = model.DepartmentId;
            await _doctorService.UpdateDoctorAsync(doctor);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
        return View(new DoctorViewModel
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            LicenseNumber = doctor?.LicenseNumber ?? "Not set",
            Specialty = doctor?.Specialty ?? "Not set",
            DepartmentName = doctor?.Department?.DepartmentName ?? "Not assigned"
        });
    }

    // GET: Doctor/Delete/5 (For confirmation)
    public async Task<IActionResult> Delete(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);

        return View(new DoctorViewModel
        {
            UserId = user.Id,
            FullName = user.FullName,
            LicenseNumber = doctor?.LicenseNumber ?? "Not set",
            DepartmentName = doctor?.Department?.DepartmentName ?? "Not assigned"
        });
    }

    // POST: Doctor/Delete/5 (Actual deletion)
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
        if (doctor != null)
        {
            await _doctorService.DeleteDoctorAsync(doctor.DoctorId);
        }

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded ? RedirectToAction(nameof(Index)) : View("Error");
    }
}