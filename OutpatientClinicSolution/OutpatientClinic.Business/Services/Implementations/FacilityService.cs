using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class FacilityService : IFacilityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FacilityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Facility>> GetAllFacilitiesAsync() =>
            await _unitOfWork.Repository<Facility>().GetAllAsync();

        public async Task<Facility> GetFacilityByIdAsync(int id) =>
            await _unitOfWork.Repository<Facility>().GetByIdAsync(id);

        public async Task<Facility> CreateFacilityAsync(Facility facility)
        {
            await _unitOfWork.Repository<Facility>().AddAsync(facility);
            await _unitOfWork.CompleteAsync();
            return facility;
        }

        public async Task<bool> UpdateFacilityAsync(Facility facility)
        {
            _unitOfWork.Repository<Facility>().Update(facility);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteFacilityAsync(int id)
        {
            var facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(id);
            if (facility == null)
                return false;
            _unitOfWork.Repository<Facility>().Delete(facility);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Facility>> FindFacilitiesAsync(string facilityName) =>
            await _unitOfWork.Repository<Facility>().FindAsync(f => f.FacilityName.Contains(facilityName));

        public async Task<int> CountFacilitiesAsync() =>
            await _unitOfWork.Repository<Facility>().CountAsync();

        public async Task<Facility?> GetFacilityByNameAsync(string name)
        {
            var facilities = await _unitOfWork.Repository<Facility>().FindAsync(f => f.FacilityName == name);
            return facilities.FirstOrDefault();
        }
    }
}
