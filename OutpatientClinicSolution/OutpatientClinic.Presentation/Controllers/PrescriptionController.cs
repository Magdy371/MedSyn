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
    public class PrescriptionController : Controller
    {
        private readonly OutpatientClinicDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PrescriptionController(OutpatientClinicDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Prescription/Index
        public async Task<IActionResult> Index()
        {
            var prescriptions = await _context.Prescriptions
                .Include(p => p.Record).ThenInclude(r => r.Appointment).ThenInclude(a => a.Patient)
                .Where(p => p.IsDeleted != true)
                .ToListAsync();

            var createdByIds = prescriptions.Select(p => p.CreatedBy).Distinct().ToList();
            var creators = await _userManager.Users
                .Where(u => createdByIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u);
            var creatorRoles = new Dictionary<string, string>();
            foreach (var user in creators.Values)
            {
                var roles = await _userManager.GetRolesAsync(user);
                creatorRoles[user.Id] = roles.FirstOrDefault() ?? "Unknown";
            }

            var viewModel = prescriptions.Select(p => new PrescriptionIndexViewModel
            {
                PrescriptionId = p.PrescriptionId,
                RecordId = p.RecordId,
                PatientName = $"{p.Record.Appointment.Patient.FirstName} {p.Record.Appointment.Patient.LastName}",
                MedicalName = p.MedicalName,
                Dosage = p.Dosage,
                Instructions = p.Instructions ?? "N/A",
                CreatedBy = p.CreatedBy,
                CreatorName = creators[p.CreatedBy].UserName,
                CreatorRole = creatorRoles[p.CreatedBy],
                CreatedDate = p.CreatedDate ?? DateTime.Now
            }).ToList();

            return View(viewModel);
        }

        // GET: Prescription/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var prescription = await _context.Prescriptions
                .Include(p => p.Record).ThenInclude(r => r.Appointment).ThenInclude(a => a.Patient)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id && p.IsDeleted != true);
            if (prescription == null) return NotFound();

            return View(prescription);
        }

        // GET: Prescription/Create
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Create()
        {
            var now = DateTime.Now;
            var windowStart = now.AddHours(-24);
            var windowEnd = now.AddHours(24);

            var records = await _context.MedicalRecords
                .Include(r => r.Appointment).ThenInclude(a => a.Patient)
                .Where(r => r.IsDeleted != true &&
                            r.CreatedDate >= windowStart && r.CreatedDate <= windowEnd)
                .OrderByDescending(r => r.CreatedDate)
                .Select(r => new
                {
                    r.RecordId,
                    DisplayText = $"{r.Appointment.AppointmentDateTime} - {r.Appointment.Patient.FirstName} {r.Appointment.Patient.LastName}"
                })
                .ToListAsync();

            ViewBag.MedicalRecords = new SelectList(records, "RecordId", "DisplayText");
            return View(new Prescription());
        }

        // POST: Prescription/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Create([Bind("RecordId,MedicalName,Dosage,Instructions")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                prescription.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                prescription.CreatedDate = DateTime.Now;
                prescription.IsDeleted = false;

                _context.Add(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var now = DateTime.Now;
            var records = await _context.MedicalRecords
                .Include(r => r.Appointment).ThenInclude(a => a.Patient)
                .Where(r => r.IsDeleted != true &&
                            r.CreatedDate >= now.AddHours(-24) && r.CreatedDate <= now.AddHours(24))
                .OrderByDescending(r => r.CreatedDate)
                .Select(r => new { r.RecordId, DisplayText = $"{r.Appointment.AppointmentDateTime} - {r.Appointment.Patient.FirstName} {r.Appointment.Patient.LastName}" })
                .ToListAsync();
            ViewBag.MedicalRecords = new SelectList(records, "RecordId", "DisplayText", prescription.RecordId);

            return View(prescription);
        }

        // GET: Prescription/Edit/5
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var prescription = await _context.Prescriptions.FirstOrDefaultAsync(p => p.PrescriptionId == id && p.IsDeleted != true);
            if (prescription == null) return NotFound();

            return View(prescription);
        }

        // POST: Prescription/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("PrescriptionId,RecordId,MedicalName,Dosage,Instructions,CreatedBy,CreatedDate")] Prescription prescription)
        {
            if (id != prescription.PrescriptionId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    prescription.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    prescription.UpdatedDate = DateTime.Now;
                    _context.Update(prescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Prescriptions.AnyAsync(p => p.PrescriptionId == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(prescription);
        }

        // GET: Prescription/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var prescription = await _context.Prescriptions
                .Include(p => p.Record).ThenInclude(r => r.Appointment).ThenInclude(a => a.Patient)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id && p.IsDeleted != true);
            if (prescription == null) return NotFound();

            return View(prescription);
        }

        // POST: Prescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription != null)
            {
                prescription.IsDeleted = true;
                prescription.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                prescription.UpdatedDate = DateTime.Now;
                _context.Update(prescription);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}