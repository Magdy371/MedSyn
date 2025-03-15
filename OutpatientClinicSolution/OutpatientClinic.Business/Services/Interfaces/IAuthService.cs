using OutpatientClinic.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OutpatientClinic.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateToken(ApplicationUser user, string role);
    }
}
