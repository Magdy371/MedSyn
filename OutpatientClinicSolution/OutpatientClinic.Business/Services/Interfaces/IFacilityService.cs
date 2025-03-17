using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IFacilityService
    {
        Task<IEnumerable<Facility>> GetAllFacilitiesAsync();
        Task<Facility> GetFacilityByIdAsync(int id);
        Task<Facility> CreateFacilityAsync(Facility facility);
        Task<bool> UpdateFacilityAsync(Facility facility);
        Task<bool> DeleteFacilityAsync(int id);
        Task<IEnumerable<Facility>> FindFacilitiesAsync(string facilityName);
        Task<int> CountFacilitiesAsync();
        Task<Facility?> GetFacilityByNameAsync(string name);
    }
}
