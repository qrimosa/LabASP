using Lab0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AlbumsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlbumsApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AlbumsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumModel>>> GetAlbums()
        {
            var albums = await _context.Albums
                .Include(a => a.Label)
                .AsNoTracking()
                .ToListAsync();
            return Ok(albums);
        }

        // POST: api/AlbumsApi
        [HttpPost]
        public async Task<ActionResult<AlbumModel>> PostAlbum([FromBody] AlbumModel album)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlbums), new { id = album.Id }, album);
        }
    }
}