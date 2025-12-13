using Lab0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab0.Controllers
{
    [Authorize(Roles = "admin")]
    public class LabelController : Controller
    {
        private readonly AppDbContext _context;

        public LabelController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Label
        public async Task<IActionResult> Index()
        {
            var labels = await _context.Labels
                .OrderBy(l => l.Name)
                .ToListAsync();
            return View(labels);
        }

        // GET: /Label/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var label = await _context.Labels
                .Include(l => l.Albums)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (label == null)
                return NotFound();

            return View(label);
        }

        // GET: /Label/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Label/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Label model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Labels.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Label/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var label = await _context.Labels.FindAsync(id);
            if (label == null)
                return NotFound();

            return View(label);
        }

        // POST: /Label/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Label model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existing = await _context.Labels.FindAsync(model.Id);
            if (existing == null)
                return NotFound();

            existing.Name = model.Name;
            existing.Country = model.Country;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Label/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var label = await _context.Labels.FindAsync(id);
            if (label == null)
                return NotFound();

            return View(label);
        }

        // POST: /Label/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var label = await _context.Labels.FindAsync(id);
            if (label != null)
            {
                _context.Labels.Remove(label);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
