using Lab0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab0.Controllers
{
    [Authorize(Roles = "admin")]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _service;
        private readonly AppDbContext _context;

        public AlbumController(IAlbumService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        // GET: list of albums with pagination
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10;
            var query = _service.GetAlbumsQuery().OrderBy(a => a.Name);

            var paginated = await PaginatedList<AlbumModel>.CreateAsync(
                query, pageNumber ?? 1, pageSize);

            return View(paginated);
        }

        // GET: display the form
        [HttpGet]
        public IActionResult Create()
        {
            PopulateLabelsDropDownList();
            return View();
        }

        // POST: receive form data and save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AlbumModel model, string Songs)
        {
            if (!ModelState.IsValid)
            {
                PopulateLabelsDropDownList(model.LabelId);
                return View(model);
            }

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

            PopulateLabelsDropDownList(album.LabelId);
            return View(album);
        }

        // POST: update album
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AlbumModel model, string Songs)
        {
            if (!ModelState.IsValid)
            {
                PopulateLabelsDropDownList(model.LabelId);
                return View(model);
            }

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
                PopulateLabelsDropDownList(model.LabelId);
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

        private void PopulateLabelsDropDownList(object? selectedLabel = null)
        {
            var labelsQuery = _context.Labels
                .OrderBy(l => l.Name)
                .AsNoTracking()
                .ToList();

            ViewBag.LabelId = new SelectList(labelsQuery, "Id", "Name", selectedLabel);
        }
        
        [HttpGet]
        public async Task<IActionResult> ApiCreate()
        {
            var labels = await _context.Labels
                .OrderBy(l => l.Name)
                .AsNoTracking()
                .ToListAsync();

            ViewBag.Labels = labels;
            return View();
        }

    }
}
