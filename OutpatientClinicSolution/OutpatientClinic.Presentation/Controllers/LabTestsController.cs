using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize] // Requires authentication for all actions
    public class LabTestsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public LabTestsController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: LabTests (Index) - Doctors and Admins only
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Index(string testNameFilter)
        {
            //var labTests = _unitOfWork.Repository<LabTest>().Query()
            //    .Include(lt => lt.Patient)
            //    .Where(lt => !lt.IsDeleted.HasValue || !lt.IsDeleted.Value);

            //if (!string.IsNullOrEmpty(testNameFilter))
            //{
            //    labTests = labTests.Where(lt => lt.TestName == testNameFilter);
            //}

            //var testNames = await _unitOfWork.Repository<LabTest>().Query()
            //    .Select(lt => lt.TestName)
            //    .Distinct()
            //    .ToListAsync();

            //ViewBag.TestNames = testNames;
            //return View(await labTests.ToListAsync());
            var labTests = await _unitOfWork.Repository<LabTest>().Query()
            .Include(lt => lt.Patient)
            .Where(lt => !lt.IsDeleted.HasValue || !lt.IsDeleted.Value)
            .ToListAsync();

            return View(labTests);
        }

        // GET: LabTests/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var labTest = await _unitOfWork.Repository<LabTest>().Query()
                .Include(lt => lt.Patient)
                .FirstOrDefaultAsync(lt => lt.TestId == id && (!lt.IsDeleted.HasValue || !lt.IsDeleted.Value));

            if (labTest == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            // Patients can only view their own lab tests
            if (roles.Contains("Patient") && labTest.PatientId.ToString() != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }

            // Doctors, Admins, Receptionists, and Patients (if authorized) can view
            if (!roles.Contains("Doctor") && !roles.Contains("Admin") && !roles.Contains("Receptionist") && !roles.Contains("Patient"))
            {
                return Forbid();
            }

            return View(labTest);
        }

        // GET: LabTests/Create - Doctors and Admins only
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Create()
        {
            var patients = await _unitOfWork.Repository<Patient>().Query()
                .Select(p => new { p.PatientId, FullName = p.FirstName + " " + p.LastName })
                .ToListAsync();
            ViewBag.Patients = new SelectList(patients, "PatientId", "FullName");
            return View();
        }
        // POST: LabTests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Create(LabTest labTest)
        {
            // Explicitly validate TestDate range
            if (labTest.TestDate < new DateTime(1753, 1, 1))
            {
                ModelState.AddModelError("TestDate", "Test date must be after 1753-01-01.");
            }

            if (ModelState.IsValid)
            {
                labTest.CreatedBy = User.Identity.Name;
                labTest.CreatedDate = DateTime.Now;
                await _unitOfWork.Repository<LabTest>().AddAsync(labTest);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate ViewBag.Patients if validation fails
            var patients = await _unitOfWork.Repository<Patient>().Query()
                .Select(p => new { p.PatientId, FullName = p.FirstName + " " + p.LastName })
                .ToListAsync();
            ViewBag.Patients = new SelectList(patients, "PatientId", "FullName", labTest.PatientId);
            return View(labTest);
        }

        // AJAX action to get appointments for a patient on a specific date or the day before
        [HttpGet]
        public async Task<IActionResult> GetAppointments(int patientId, DateTime testDate)
        {
            // Validate testDate to ensure it's within SQL Server's datetime range
            if (testDate < new DateTime(1753, 1, 1))
            {
                return Json(new List<object>()); // Return empty list to avoid SQL error
            }

            var appointments = await _unitOfWork.Repository<Appointment>().Query()
                .Where(a => a.PatientId == patientId &&
                            (a.AppointmentDateTime.Date == testDate.Date ||
                             a.AppointmentDateTime.Date == testDate.Date.AddDays(-1)))
                .OrderByDescending(a => a.AppointmentDateTime)
                .Select(a => new { a.AppointmentId, a.AppointmentDateTime })
                .ToListAsync();

            return Json(appointments);
        }

        // GET: LabTests/Edit/5 - Doctors and Admins only
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var labTest = await _unitOfWork.Repository<LabTest>().GetByIdAsync(id);
            if (labTest == null || labTest.IsDeleted == true)
            {
                return NotFound();
            }
            return View(labTest);
        }

        // POST: LabTests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Edit(int id, LabTest labTest)
        {
            if (id != labTest.TestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    labTest.UpdatedBy = User.Identity.Name;
                    labTest.UpdatedDate = DateTime.Now;
                    _unitOfWork.Repository<LabTest>().Update(labTest);
                    await _unitOfWork.CompleteAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LabTestExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(labTest);
        }

        // GET: LabTests/Delete/5 - Doctors and Admins only
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var labTest = await _unitOfWork.Repository<LabTest>().Query()
                .Include(lt => lt.Patient)
                .FirstOrDefaultAsync(lt => lt.TestId == id && (!lt.IsDeleted.HasValue || !lt.IsDeleted.Value));

            if (labTest == null)
            {
                return NotFound();
            }
            return View(labTest);
        }

        // POST: LabTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labTest = await _unitOfWork.Repository<LabTest>().GetByIdAsync(id);
            if (labTest != null)
            {
                labTest.IsDeleted = true;
                labTest.UpdatedBy = User.Identity.Name;
                labTest.UpdatedDate = DateTime.Now;
                _unitOfWork.Repository<LabTest>().Update(labTest);
                await _unitOfWork.CompleteAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LabTestExists(int id)
        {
            return await _unitOfWork.Repository<LabTest>().ExistsAsync(lt => lt.TestId == id && (!lt.IsDeleted.HasValue || !lt.IsDeleted.Value));
        }
    }
}