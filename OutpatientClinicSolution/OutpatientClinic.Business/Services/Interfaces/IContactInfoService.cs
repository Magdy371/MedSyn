using OutpatientClinic.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IContactInfoService
    {
        Task<IEnumerable<ContactInfo>> GetAllContactInfoAsync();
        Task<ContactInfo> GetContactInfoByIdAsync(int id);
        Task<ContactInfo> CreateContactInfoAsync(ContactInfo contactInfo);
        Task<bool> UpdateContactInfoAsync(ContactInfo contactInfo);
        Task<bool> DeleteContactInfoAsync(int id);
        Task<IEnumerable<ContactInfo>> FindContactInfoAsync(string email);
        Task<int> CountContactInfoAsync();
        Task<ContactInfo?> GetContactInfoByEmailAsync(string email);
    }
}
