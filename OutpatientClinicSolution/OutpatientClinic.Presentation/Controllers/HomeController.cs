//using Microsoft.AspNetCore.Mvc;
//using OutpatientClinic.Presentation.Models;
//using System.Diagnostics;

//namespace OutpatientClinic.Presentation.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}

// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.DataAccess.Context;
using OutpatientClinic.Presentation.Models;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly OutpatientClinicDbContext _context;

        public HomeController(OutpatientClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new LandingPageViewModel
            {
                PatientCount = await _context.Patients.Where(p => p.IsDeleted != true).CountAsync(),
                StaffCount = await _context.Staff.Where(s => s.IsDeleted != true).CountAsync(),
                ClinicCount = await _context.Clinics.Where(c => c.IsDeleted != true).CountAsync(),
                AppointmentCount = await _context.Appointments.Where(a => a.IsDeleted != true).CountAsync(),
                DepartmentCount = await _context.Departments.Where(d => d.IsDeleted != true).CountAsync(),
                DoctorCount = await _context.Doctors
                    .Where(d => d.DoctorNavigation.IsDeleted != true)
                    .CountAsync()

            };

            return View(viewModel);
        }
    }
}
