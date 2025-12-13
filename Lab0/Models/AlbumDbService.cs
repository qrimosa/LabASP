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

        // New: query for pagination, includes Label
        public IQueryable<AlbumModel> GetAlbumsQuery()
        {
            return _context.Albums
                .Include(a => a.Label)
                .AsNoTracking();
        }

        public List<AlbumModel> GetAlbums()
        {
            return GetAlbumsQuery().ToList();
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

                _context.Entry(existing).CurrentValues.SetValues(album);
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
            return _context.Albums
                .Include(a => a.Label)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
