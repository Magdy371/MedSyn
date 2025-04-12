using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.DataAccess.Context;
using OutpatientClinic.DataAccess.Entities;
using OutpatientClinic.Presentation.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize]
    public class MedicalRecordController : Controller
    {
        private readonly OutpatientClinicDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicalRecordController(OutpatientClinicDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MedicalRecord/Index
        public async Task<IActionResult> Index()
        {
            var records = await _context.MedicalRecords
                .Include(m => m.Appointment).ThenInclude(a => a.Patient)
                .Where(m => m.IsDeleted != true)
                .ToListAsync();

            var createdByIds = records.Select(m => m.CreatedBy).Distinct().ToList();
            var creators = await _userManager.Users
                .Where(u => createdByIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u);
            var creatorRoles = new Dictionary<string, string>();
            foreach (var user in creators.Values)
            {
                var roles = await _userManager.GetRolesAsync(user);
                creatorRoles[user.Id] = roles.FirstOrDefault() ?? "Unknown";
            }

            var viewModel = records.Select(m => new MedicalRecordIndexViewModel
            {
                RecordId = m.RecordId,
                AppointmentId = m.AppointmentId ?? 0,
                PatientName = $"{m.Appointment.Patient.FirstName} {m.Appointment.Patient.LastName}",
                Diagnosis = m.Diagnosis,
                Treatment = m.Treatment,
                CreatedBy = m.CreatedBy,
                CreatorName = m.CreatedBy != null && creators.ContainsKey(m.CreatedBy) ? creators[m.CreatedBy].UserName : "Unknown",
                CreatorRole = m.CreatedBy != null && creatorRoles.ContainsKey(m.CreatedBy) ? creatorRoles[m.CreatedBy] : "Unknown",
                CreatedDate = m.CreatedDate ?? DateTime.Now
            }).ToList();


            return View(viewModel);
        }

        // GET: MedicalRecord/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var record = await _context.MedicalRecords
                .Include(m => m.Appointment).ThenInclude(a => a.Patient)
                .FirstOrDefaultAsync(m => m.RecordId == id && m.IsDeleted != true);
            if (record == null) return NotFound();

            return View(record);
        }

        // GET: MedicalRecord/Create
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Create()
        {
            var now = DateTime.Now;
            var windowStart = now.AddHours(-24);
            var windowEnd = now.AddHours(24);

            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.IsDeleted != true && a.MedicalRecord == null &&
                            a.AppointmentDateTime >= windowStart && a.AppointmentDateTime <= windowEnd)
                .OrderByDescending(a => a.AppointmentDateTime)
                .Select(a => new
                {
                    a.AppointmentId,
                    DisplayText = $"{a.AppointmentDateTime} - {a.Patient.FirstName} {a.Patient.LastName}"
                })
                .ToListAsync();

            ViewBag.Appointments = new SelectList(appointments, "AppointmentId", "DisplayText");
            return View(new MedicalRecord());
        }

        // POST: MedicalRecord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Create([Bind("AppointmentId,Diagnosis,Treatment")] MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                medicalRecord.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                medicalRecord.CreatedDate = DateTime.Now;
                medicalRecord.IsDeleted = false;

                _context.Add(medicalRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var now = DateTime.Now;
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.IsDeleted != true && a.MedicalRecord == null &&
                            a.AppointmentDateTime >= now.AddHours(-24) && a.AppointmentDateTime <= now.AddHours(24))
                .OrderByDescending(a => a.AppointmentDateTime)
                .Select(a => new { a.AppointmentId, DisplayText = $"{a.AppointmentDateTime} - {a.Patient.FirstName} {a.Patient.LastName}" })
                .ToListAsync();
            ViewBag.Appointments = new SelectList(appointments, "AppointmentId", "DisplayText", medicalRecord.AppointmentId);

            return View(medicalRecord);
        }

        // GET: MedicalRecord/Edit/5
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var record = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.RecordId == id && m.IsDeleted != true);
            if (record == null) return NotFound();

            return View(record);
        }

        // POST: MedicalRecord/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,AppointmentId,Diagnosis,Treatment,CreatedBy,CreatedDate")] MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.RecordId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    medicalRecord.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    medicalRecord.UpdatedDate = DateTime.Now;
                    _context.Update(medicalRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.MedicalRecords.AnyAsync(m => m.RecordId == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medicalRecord);
        }

        // GET: MedicalRecord/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var record = await _context.MedicalRecords
                .Include(m => m.Appointment).ThenInclude(a => a.Patient)
                .FirstOrDefaultAsync(m => m.RecordId == id && m.IsDeleted != true);
            if (record == null) return NotFound();

            return View(record);
        }

        // POST: MedicalRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record != null)
            {
                record.IsDeleted = true;
                record.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                record.UpdatedDate = DateTime.Now;
                _context.Update(record);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        //Search filter 
        public async Task<IActionResult> Search(string searchTerm, string roleFilter)
        {
            var records = await _context.MedicalRecords
                .Include(m => m.Appointment).ThenInclude(a => a.Patient)
                .Where(m => m.IsDeleted != true &&
                    (string.IsNullOrEmpty(searchTerm) ||
                     m.Appointment.Patient.FirstName.Contains(searchTerm) ||
                     m.Appointment.Patient.LastName.Contains(searchTerm) ||
                     m.Diagnosis.Contains(searchTerm)))
                .ToListAsync();

            var createdByIds = records.Select(m => m.CreatedBy).Distinct().ToList();
            var creators = await _userManager.Users
                .Where(u => createdByIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u);

            var creatorRoles = new Dictionary<string, string>();
            foreach (var user in creators.Values)
            {
                var roles = await _userManager.GetRolesAsync(user);
                creatorRoles[user.Id] = roles.FirstOrDefault() ?? "Unknown";
            }

            if (!string.IsNullOrEmpty(roleFilter))
            {
                records = records.Where(r =>
                    r.CreatedBy != null &&
                    creatorRoles.TryGetValue(r.CreatedBy, out var role) &&
                    role == roleFilter).ToList();
            }

            var viewModel = records.Select(m => new MedicalRecordIndexViewModel
            {
                RecordId = m.RecordId,
                AppointmentId = m.AppointmentId ?? 0,
                PatientName = $"{m.Appointment.Patient.FirstName} {m.Appointment.Patient.LastName}",
                Diagnosis = m.Diagnosis,
                Treatment = m.Treatment,
                CreatedBy = m.CreatedBy,
                CreatorName = m.CreatedBy != null && creators.ContainsKey(m.CreatedBy) ? creators[m.CreatedBy].UserName : "Unknown",
                CreatorRole = m.CreatedBy != null && creatorRoles.ContainsKey(m.CreatedBy) ? creatorRoles[m.CreatedBy] : "Unknown",
                CreatedDate = m.CreatedDate ?? DateTime.Now
            }).ToList();

            return PartialView("_MedicalRecordTable", viewModel);
        }


        public async Task<IActionResult> DetailsPartial(int id)
        {
            var record = await _context.MedicalRecords
                .Include(m => m.Appointment).ThenInclude(a => a.Patient)
                .FirstOrDefaultAsync(m => m.RecordId == id && m.IsDeleted != true);

            return PartialView("_DetailsPartial", record);
        }


    }

}