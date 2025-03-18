using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.DTOs;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // Dashboard view displaying summary statistics
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalUsers = await _adminService.GetTotalUsersAsync();
            ViewBag.PendingAppointments = await _adminService.GetPendingAppointmentsCountAsync();
            ViewBag.TotalRevenue = await _adminService.GetTotalRevenueAsync();

            return View("AdminIndex_Test");
        }

        // View for managing users
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return View(users);
        }

        // Details view for a specific user
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            return user != null ? View(user) : NotFound();
        }

        // Assign a role to a user
        [HttpPost]
        public async Task<IActionResult> AssignRole(RoleAssignmentDto model)
        {
            var result = await _adminService.AssignRoleAsync(model.UserId, model.Role);
            if (result)
                return RedirectToAction("ManageUsers");
            else
                return BadRequest("Invalid user or role.");
        }

        // Remove a user from the system
        [HttpPost]
        public async Task<IActionResult> RemoveUser(string id)
        {
            var result = await _adminService.RemoveUserAsync(id);
            if (result)
                return RedirectToAction("ManageUsers");
            else
                return NotFound("User not found.");
        }

        // Get all available roles
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _adminService.GetRolesAsync();
            return View(roles);
        }
    }
}
