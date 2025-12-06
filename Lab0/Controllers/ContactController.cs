using Lab0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab0.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;
    private readonly IOrganizationService _organizationService;

    public ContactController(IContactService contactService, IOrganizationService organizationService)
    {
        _contactService = contactService;
        _organizationService = organizationService;
    }

    // GET
    public IActionResult Index()
    {
        return View(_contactService.GetContacts());
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Organizations = new SelectList(
            _organizationService.GetOrganizations(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public IActionResult Create(Contact model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Organizations = new SelectList(
                _organizationService.GetOrganizations(), "Id", "Name", model.OrganizationId);
            return View(model);
        }

        _contactService.AddContact(model);
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var contact = _contactService.GetContactById(id);
        if (contact is null)
            return NotFound();

        return View(contact);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var contact = _contactService.GetContactById(id);
        if (contact is null)
            return NotFound();

        ViewBag.Organizations = new SelectList(
            _organizationService.GetOrganizations(), "Id", "Name", contact.OrganizationId);
        return View(contact);
    }

    [HttpPost]
    public IActionResult Edit(Contact model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Organizations = new SelectList(
                _organizationService.GetOrganizations(), "Id", "Name", model.OrganizationId);
            return View(model);
        }

        _contactService.UpdateContact(model);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var contact = _contactService.GetContactById(id);
        if (contact is null)
            return NotFound();

        return View(contact);
    }

    [HttpPost]
    public IActionResult DeleteConfirm(int id)
    {
        _contactService.DeleteContactById(id);
        return RedirectToAction("Index");
    }
}
