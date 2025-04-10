using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.DataAccess.Context;
using OutpatientClinic.Presentation.Models;

namespace OutpatientClinic.Presentation.Controllers
{
    public class ClinicViewController : Controller
    {
        private readonly OutpatientClinicDbContext _context;

        public ClinicViewController(OutpatientClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var clinics = await _context.Clinics
                .Where(c => c.IsDeleted != true)
                .Select(c => new ClinicCardModel
                {
                    Id = c.ClinicId,
                    Name = c.ClinicName
                })
                .ToListAsync();

            return View(clinics);
        }
    }
}
