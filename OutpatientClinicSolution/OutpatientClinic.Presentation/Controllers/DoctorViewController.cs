// Controllers/DoctorsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.DataAccess.Context;
using OutpatientClinic.Presentation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    public class DoctorViewController : Controller
    {
        private readonly OutpatientClinicDbContext _context;

        public DoctorViewController(OutpatientClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _context.Doctors
                .Include(d => d.DoctorNavigation)
                .Include(d => d.Department)
                .Where(d => d.DoctorNavigation.IsDeleted != true)
                .Select(d => new DoctorCardModel
                {
                    FullName = d.DoctorNavigation.FullName,
                    DepartmentName = d.Department.DepartmentName
                })
                .ToListAsync();

            return View(doctors);
        }
    }
}