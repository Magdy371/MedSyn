// Controllers/MedicineController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.DataAccess.Context;
using OutpatientClinic.DataAccess.Entities;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Doctor")]
    public class MedicineController : Controller
    {
        private readonly OutpatientClinicDbContext _context;

        public MedicineController(OutpatientClinicDbContext context)
        {
            _context = context;
        }

        // GET: Medicine
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicines
                .Where(m => m.IsDeleted != true)
                .OrderBy(m => m.Name)
                .ToListAsync());
        }

        // GET: Medicine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var medicine = await _context.Medicines
                .FirstOrDefaultAsync(m => m.MedicineId == id && m.IsDeleted != true);

            return medicine == null ? NotFound() : View(medicine);
        }

        // GET: Medicine/Create
        public IActionResult Create()
        {
            PopulateTypeDropdown();
            return View();
        }

        // POST: Medicine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicineId,Name,Type,DefaultDosage,ForAdult,ForChildren,Description")] Medicine medicine)
        {
            if (!ModelState.IsValid) return View(medicine);

            medicine.CreatedDate = DateTime.Now;
            medicine.IsDeleted = false;

            _context.Add(medicine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Medicine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null || medicine.IsDeleted == true) return NotFound();

            PopulateTypeDropdown();
            return View(medicine);
        }

        // POST: Medicine/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicineId,Name,Type,DefaultDosage,ForAdult,ForChildren,Description,CreatedDate")] Medicine medicine)
        {
            if (id != medicine.MedicineId) return NotFound();

            if (!ModelState.IsValid) return View(medicine);

            try
            {
                medicine.UpdatedDate = DateTime.Now;
                _context.Update(medicine);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(medicine.MedicineId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Medicine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var medicine = await _context.Medicines
                .FirstOrDefaultAsync(m => m.MedicineId == id && m.IsDeleted != true);

            return medicine == null ? NotFound() : View(medicine);
        }

        // POST: Medicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine != null)
            {
                medicine.IsDeleted = true;
                medicine.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MedicineExists(int id) =>
            _context.Medicines.Any(e => e.MedicineId == id);

        private void PopulateTypeDropdown()
        {
            ViewBag.MedicineTypes = new SelectList(new List<string>
            {
                "Tablet", "Capsule", "Liquid", "Injection",
                "Inhaler", "Cream", "Syrup", "Chewable"
            });
        }
    }
}