using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class ContactController : Controller
{
    private static Dictionary<int, Contact> _contacts = new()
    {
        {1, new Contact()
            {
                Id = 1,
                Name = "John Doe",
                Email = "johndoe@gmail.com",
                BirthDate = DateOnly.FromDateTime(new DateTime(1992, 05, 01))
        }},
        {2, new Contact()
            {
                Id = 2,
                Name = "Sigma Sigmovich",
                Email = "sigma@gmail.com",
                BirthDate = DateOnly.FromDateTime(new DateTime(1967, 06, 07))
        }},
    };

    private static int i = 0;
    // GET
    public IActionResult Index()
    {
        return View(_contacts.Values.ToList());
    }
    [HttpGet]
    public IActionResult Create()
    {
        return  View();
    }

    [HttpPost]
    public IActionResult Create(Contact contact)
    {
        if (ModelState.IsValid)
        {
            contact.Id = ++i;
            _contacts.Add(contact.Id, contact);
            return RedirectToAction("Index");
        }

        return View(contact);
    }

    public IActionResult Details(int id)
    {
        if (_contacts.ContainsKey(id))
        {
            return View(_contacts[id]);
        }
        else
        {
            return NotFound();
        }
    }
}