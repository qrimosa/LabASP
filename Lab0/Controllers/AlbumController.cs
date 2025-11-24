using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _service;

        public AlbumController(IAlbumService service)
        {
            _service = service;
        }

        // GET: list of albums
        public IActionResult Index()
        {
            return View(_service.GetAlbums());
        }

        // GET: display the form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: receive form data and save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AlbumModel model, string Songs)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!string.IsNullOrWhiteSpace(Songs))
                model.Songs = Songs.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList();

            _service.AddAlbum(model);
            return RedirectToAction("Index");
        }

        // GET: album details
        public IActionResult Details(int id)
        {
            var album = _service.GetAlbumById(id);

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
            var album = _service.GetAlbumById(id);

            if (album is null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: update album
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AlbumModel model, string Songs)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!string.IsNullOrWhiteSpace(Songs))
                model.Songs = Songs.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList();
            else
                model.Songs = new List<string>();

            var ok = _service.UpdateAlbum(model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update album (it may not exist).");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // GET: delete confirmation page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var album = _service.GetAlbumById(id);

            if (album is null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: delete action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            _service.DeleteAlbumById(id);
            return RedirectToAction("Index");
        }
    }
}
