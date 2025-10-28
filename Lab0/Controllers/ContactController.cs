using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactInterface _contactInterface;

        public ContactController(IContactInterface contactInterface)
        {
            _contactInterface = contactInterface;
        }

        // GET: /Contact
        public IActionResult Index()
        {
            var contacts = _contactInterface.GetContacts();
            return View(contacts);
        }

        // GET: /Contact/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactInterface.CreateContact(contact);
                return RedirectToAction(nameof(Index));
            }

            return View(contact);
        }

        // GET: /Contact/Details/5
        public IActionResult Details(int id)
        {
            var contact = _contactInterface.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: /Contact/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contact = _contactInterface.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: /Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                bool updated = _contactInterface.UpdateContact(contact);
                if (!updated)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(contact);
        }

        // POST: /Contact/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            bool deleted = _contactInterface.DeleteContact(id);
            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
