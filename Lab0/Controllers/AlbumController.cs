using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class AlbumController(IAlbumService service) : Controller
{
    // GET: list of albums
    public IActionResult Index()
    {
        return View(service.GetAlbums());
    }

    // GET: display the form
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: receive form data and save
    [HttpPost]
    public IActionResult Create(AlbumModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        service.AddAlbum(model);
        return RedirectToAction("Index");
    }

    // GET: album details
    public IActionResult Details(int id)
    {
        var album = service.GetAlbumById(id);

        if (album is null)
        {
            return NotFound();
        }

        return View(album);
    }

    // GET: edit form
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var album = service.GetAlbumById(id);

        if (album is null)
        {
            return NotFound();
        }

        return View(album);
    }

    // POST: update album
    [HttpPost]
    public IActionResult Edit(AlbumModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        service.UpdateAlbum(model);
        return RedirectToAction("Index");
    }

    // GET: delete confirmation
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var album = service.GetAlbumById(id);

        if (album is null)
        {
            return NotFound();
        }

        return View(album);
    }

    // POST: delete action
    [HttpPost]
    public IActionResult DeleteConfirm(int id)
    {
        service.DeleteAlbumById(id);
        return RedirectToAction("Index");
    }
}