using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.DTOs;
using OutpatientClinic.DataAccess.Context;
using OutpatientClinic.DataAccess.Entities;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly OutpatientClinicDbContext _context;

        public AdminService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, OutpatientClinicDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _userManager.Users.CountAsync();
        }

        public async Task<int> GetPendingAppointmentsCountAsync()
        {
            return await _context.Appointments.CountAsync(a => a.Status == "Pending");
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Billings.Where(b => b.PaymentStatus == "Paid").SumAsync(b => b.Amount);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> AssignRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || !await _roleManager.RoleExistsAsync(role)) return false;

            await _userManager.AddToRoleAsync(user, role);
            return true;
        }

        public async Task<bool> RemoveUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<IEnumerable<string>> GetRolesAsync()
        {
            return await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        }
    }
}
