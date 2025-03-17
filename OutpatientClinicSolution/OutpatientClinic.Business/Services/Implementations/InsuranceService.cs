using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InsuranceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Insurance>> GetAllInsurancesAsync() =>
            await _unitOfWork.Repository<Insurance>().GetAllAsync();

        public async Task<Insurance> GetInsuranceByIdAsync(int id) =>
            await _unitOfWork.Repository<Insurance>().GetByIdAsync(id);

        public async Task<Insurance> CreateInsuranceAsync(Insurance insurance)
        {
            await _unitOfWork.Repository<Insurance>().AddAsync(insurance);
            await _unitOfWork.CompleteAsync();
            return insurance;
        }

        public async Task<bool> UpdateInsuranceAsync(Insurance insurance)
        {
            _unitOfWork.Repository<Insurance>().Update(insurance);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteInsuranceAsync(int id)
        {
            var insurance = await _unitOfWork.Repository<Insurance>().GetByIdAsync(id);
            if (insurance == null)
                return false;
            _unitOfWork.Repository<Insurance>().Delete(insurance);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<int> CountInsurancesAsync() =>
            await _unitOfWork.Repository<Insurance>().CountAsync();

        public async Task<Insurance?> GetInsuranceByPolicyNumberAsync(string policyNumber)
        {
            var insurances = await _unitOfWork.Repository<Insurance>().FindAsync(i => i.PolicyNumber == policyNumber);
            return insurances.FirstOrDefault();
        }
    }
}
