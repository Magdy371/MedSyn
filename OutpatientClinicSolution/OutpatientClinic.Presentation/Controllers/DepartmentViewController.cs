// Controllers/DepartmentsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.DataAccess.Context;
using OutpatientClinic.Presentation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    public class DepartmentViewController : Controller
    {
        private readonly OutpatientClinicDbContext _context;

        public DepartmentViewController(OutpatientClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var departments = 
                await _context.Departments
                .Where(d => d.IsDeleted != true)
                .Select(d => new DepartmentCardModel
                {
                    Id = d.DepartmentId,
                    Name = d.DepartmentName
                })
                .ToListAsync();

            return View(departments);
        }
    }
}