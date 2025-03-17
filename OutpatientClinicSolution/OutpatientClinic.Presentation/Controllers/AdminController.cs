using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.DTOs;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> AdminIndex()
        {
            ViewBag.TotalUsers = await _adminService.GetTotalUsersAsync();
            ViewBag.PendingAppointments = await _adminService.GetPendingAppointmentsCountAsync();
            ViewBag.TotalRevenue = await _adminService.GetTotalRevenueAsync();

            return View();
        }
        public async Task<IActionResult> AdminIndex_Test()
        {
            
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> GetUserDetails(string id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            return user != null ? View(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(RoleAssignmentDto model)
        {
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.Role))
            {
                return BadRequest("UserId and Role cannot be null or empty");
            }

            var result = await _adminService.AssignRoleAsync(model.UserId, model.Role);
            return result ? RedirectToAction("ManageUsers") : BadRequest("Invalid user or role");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(string id)
        {
            var result = await _adminService.RemoveUserAsync(id);
            return result ? RedirectToAction("ManageUsers") : NotFound("User not found");
        }

        public async Task<IActionResult> GetRoles()
        {
            var roles = await _adminService.GetRolesAsync();
            return View(roles);
        }
    }
}
