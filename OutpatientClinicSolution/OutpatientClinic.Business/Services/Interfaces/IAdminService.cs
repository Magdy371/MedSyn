using System.Collections.Generic;
using System.Threading.Tasks;
using OutpatientClinic.DataAccess.Entities;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IAdminService
    {
        // Dashboard statistics
        Task<int> GetTotalUsersAsync();
        Task<int> GetPendingAppointmentsCountAsync();
        Task<decimal> GetTotalRevenueAsync();

        // User management
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<bool> AssignRoleAsync(string userId, string role);
        Task<bool> RemoveUserAsync(string userId);
        Task<IEnumerable<string>> GetRolesAsync();
    }
}
