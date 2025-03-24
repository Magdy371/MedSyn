using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.DataAccess.Entities;
using System.Threading.Tasks;
using System.Runtime;

namespace OutpatientClinic.Presentation.Controllers
{
    [Authorize(Roles ="Admin, Staff")]
    public class ContactInfoController : Controller
    {
        private readonly IContactInfoService _contactInfoService;
        public ContactInfoController(IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }
        // GET: /ContactInfo
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactInfoService.GetAllContactInfoAsync();
            return View(contacts);
        }
        // GET: /ContactInfo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var contact = await _contactInfoService.GetContactInfoByIdAsync(id);
            if (contact == null)
                return NotFound();
            return View(contact);
        }

        // GET: /ContactInfo/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactInfo contactInfo)
        {
            if(ModelState.IsValid)
            {
                await _contactInfoService.CreateContactInfoAsync(contactInfo);
                return RedirectToAction(nameof(Index));
            }
            return View(contactInfo);
        }
        //GET: /ContactInfo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactInfoService.GetContactInfoByIdAsync(id);
            if (contact == null)
                return NotFound();

            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id , ContactInfo contactInfo)
        {
            if (id != contactInfo.ContactId)
                return BadRequest();
            if(ModelState.IsValid)
            {
                var update = await _contactInfoService.UpdateContactInfoAsync(contactInfo);
                    if (update)
                        return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Unable to update the contact info");
            }
            return View(contactInfo);
        }
        // GET: /ContactInfo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactInfoService.GetContactInfoByIdAsync(id);
            if (contact == null)
                return NotFound();
            return View(contact);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _contactInfoService.DeleteContactInfoAsync(id);
            if (!deleted)
                return BadRequest("Delete Operation Failed");
            return RedirectToAction(nameof(Index));
        }
    }
}
