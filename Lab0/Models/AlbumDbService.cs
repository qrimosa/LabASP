using Microsoft.EntityFrameworkCore;

namespace Lab0.Models
{
    public class AlbumDbService : IAlbumService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AlbumDbService> _logger;

        public AlbumDbService(AppDbContext context, ILogger<AlbumDbService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<AlbumModel> GetAlbums()
        {
            return _context.Albums.ToList();
        }

        public void AddAlbum(AlbumModel album)
        {
            _context.Albums.Add(album);
            _context.SaveChanges();
        }

        public bool UpdateAlbum(AlbumModel album)
        {
            try
            {
                var existing = _context.Albums.Find(album.Id);
                if (existing == null) return false;

                // Update scalar & simple properties
                _context.Entry(existing).CurrentValues.SetValues(album);

                // For the Songs property (List<string>) the conversion handles saving,
                // but ensure the navigation property is set on the tracked entity:
                existing.Songs = album.Songs;

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e, "Concurrency error while updating album with id {Id}", album.Id);
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while updating album with id {Id}", album.Id);
                return false;
            }

            return true;
        }

        public bool DeleteAlbumById(int id)
        {
            var deleted = _context.Albums.Find(id);
            if (deleted == null)
            {
                return false;
            }
            _context.Albums.Remove(deleted);
            _context.SaveChanges();
            return true;
        }

        public AlbumModel? GetAlbumById(int id)
        {
            return _context.Albums.Find(id);
        }
    }
}
