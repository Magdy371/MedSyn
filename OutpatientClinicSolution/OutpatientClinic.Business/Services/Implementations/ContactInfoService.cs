using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ContactInfo>> GetAllContactInfoAsync() =>
            await _unitOfWork.Repository<ContactInfo>().GetAllAsync();

        public async Task<ContactInfo> GetContactInfoByIdAsync(int id) =>
            await _unitOfWork.Repository<ContactInfo>().GetByIdAsync(id);

        public async Task<ContactInfo> CreateContactInfoAsync(ContactInfo contactInfo)
        {
            await _unitOfWork.Repository<ContactInfo>().AddAsync(contactInfo);
            await _unitOfWork.CompleteAsync();
            return contactInfo;
        }

        public async Task<bool> UpdateContactInfoAsync(ContactInfo contactInfo)
        {
            _unitOfWork.Repository<ContactInfo>().Update(contactInfo);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteContactInfoAsync(int id)
        {
            var contact = await _unitOfWork.Repository<ContactInfo>().GetByIdAsync(id);
            if (contact == null)
                return false;
            _unitOfWork.Repository<ContactInfo>().Delete(contact);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<ContactInfo>> FindContactInfoAsync(string email)
        {
            var contacts = await _unitOfWork.Repository<ContactInfo>().FindAsync(c => c.Email != null && c.Email.Contains(email));
            return contacts ?? Enumerable.Empty<ContactInfo>();
        }

        public async Task<int> CountContactInfoAsync() =>
            await _unitOfWork.Repository<ContactInfo>().CountAsync();

        public async Task<ContactInfo?> GetContactInfoByEmailAsync(string email)
        {
            var contacts = await _unitOfWork.Repository<ContactInfo>().FindAsync(c => c.Email == email);
            return contacts.FirstOrDefault();
        }
    }
}
