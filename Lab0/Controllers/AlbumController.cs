using Microsoft.AspNetCore.Mvc;
using Lab0.Models;

namespace Lab0.Controllers;

public class AlbumController : Controller
{
    private static readonly Dictionary<int, AlbumModel> _albums = new();
    private static int _nextId = 1;

    static AlbumController()
    {
        _albums[_nextId++] = new AlbumModel
        {
            Id = 1,
            Name = "Hybrid Theory",
            Band = "Linkin Park",
            Songs = new List<string> { "Papercut", "In the End", "Crawling" },
            ChartPosition = 2,
            ReleaseDate = new DateOnly(2000, 10, 24),
            TotalDuration = new TimeSpan(0, 37, 45)
        };

        _albums[_nextId++] = new AlbumModel
        {
            Id = 2,
            Name = "Random Access Memories",
            Band = "Daft Punk",
            Songs = new List<string> { "Give Life Back to Music", "Get Lucky", "Instant Crush" },
            ChartPosition = 1,
            ReleaseDate = new DateOnly(2013, 5, 17),
            TotalDuration = new TimeSpan(0, 74, 24)
        };
    }

    public IActionResult Index()
    {
        var albums = _albums.Values.ToList();
        return View(albums);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(AlbumModel album, string Songs)
    {
        if (!ModelState.IsValid)
            return View(album);

        album.Songs = Songs.Split(',', StringSplitOptions.RemoveEmptyEntries)
                           .Select(s => s.Trim())
                           .ToList();

        album.Id = _nextId++;
        _albums[album.Id] = album;

        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        if (!_albums.TryGetValue(id, out var album))
            return NotFound();

        return View(album);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _albums.Remove(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (_albums.ContainsKey(id))
        {
            return View(_albums[id]);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Edit(AlbumModel album)
    {
        if (!ModelState.IsValid)
        {
            return View(album);
        }
        _albums[album.Id] = album;
        return RedirectToAction("Index");
    }
}