using Microsoft.EntityFrameworkCore;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class BillingService : IBillingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BillingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Billing>> GetAllBillingsAsync() =>
            await _unitOfWork.Repository<Billing>().GetAllAsync();

        public async Task<Billing> GetBillingByIdAsync(int id) =>
            await _unitOfWork.Repository<Billing>().GetByIdAsync(id);

        public async Task<Billing> CreateBillingAsync(Billing billing)
        {
            await _unitOfWork.Repository<Billing>().AddAsync(billing);
            await _unitOfWork.CompleteAsync();
            return billing;
        }

        public async Task<bool> UpdateBillingAsync(Billing billing)
        {
            _unitOfWork.Repository<Billing>().Update(billing);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteBillingAsync(int id)
        {
            var billing = await _unitOfWork.Repository<Billing>().GetByIdAsync(id);
            if (billing == null)
                return false;
            _unitOfWork.Repository<Billing>().Delete(billing);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<int> CountBillingsAsync() =>
            await _unitOfWork.Repository<Billing>().CountAsync();

        public async Task<IEnumerable<Billing>> FindBillingsByStatusAsync(string status) =>
            await _unitOfWork.Repository<Billing>().FindAsync(b => b.PaymentStatus != null && b.PaymentStatus.Contains(status));

        public async Task<decimal> GetTotalRevenueAsync()
        {
            var paidBillings = await _unitOfWork.Repository<Billing>()
                .FindAsync(b => b.PaymentStatus == "Paid");

            return paidBillings.Sum(b => b.Amount);
        }


    }
}
