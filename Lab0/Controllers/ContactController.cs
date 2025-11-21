using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class ContactController(IContactService service) : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(service.GetContacts());
    }

    [HttpGet]   // wyświetlenie formularza dodania obiektu
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost] //odbior danych obiektu i zapisanie do bazy
    public IActionResult Create(Contact model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        service.AddContact(model);
        return RedirectToAction("Index");   // przejdź do listy obiektów
    }

    public IActionResult Details(int id)
    {
        var contact = service.GetContactById(id);
        if (contact is not null)
        {
            return View(contact);
        }
        else
        {
            return NotFound();
        }
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var contact = service.GetContactById(id);
        if (contact is not null)
        {
            return View(contact);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(Contact model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        // aktualizacja obiektu
        service.UpdateContact(model);
        return RedirectToAction("Index"); 
    }
    
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var contact = service.GetContactById(id);
        if (contact is not null)
        {
            return View(contact);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult DeleteConfirm(int id)
    {
        service.DeleteContactById(id);
        return RedirectToAction("Index");
    }
    
    
}