using Lab0.Models;

namespace Lab0.Models
{
    public class AlbumMemoryService : IAlbumService
    {
        private Dictionary<int, AlbumModel> _albums = new()
        {
            {
                1,
                new AlbumModel()
                {
                    Id = 1,
                    Name = "Hybrid Theory",
                    Band = "Linkin Park",
                    Songs = new List<string> { "In the End", "Crawling" },
                    ChartPosition = 2,
                    ReleaseDate = new DateOnly(2000, 10, 24),
                    TotalDuration = new TimeSpan(0, 37, 45)
                }
            },
            {
                2,
                new AlbumModel()
                {
                    Id = 2,
                    Name = "Nevermind",
                    Band = "Nirvana",
                    Songs = new List<string> { "Smells Like Teen Spirit", "Come As You Are" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1991, 9, 24),
                    TotalDuration = new TimeSpan(0, 49, 23)
                }
            }
        };

        private int currentId = 2;

        public List<AlbumModel> GetAlbums()
        {
            return _albums.Values.ToList();
        }

        public void AddAlbum(AlbumModel album)
        {
            album.Id = ++currentId;
            _albums.Add(album.Id, album);
        }

        public bool UpdateAlbum(AlbumModel album)
        {
            if (_albums.ContainsKey(album.Id))
            {
                _albums[album.Id] = album;
                return true;
            }
            return false;
        }

        public bool DeleteAlbumById(int id)
        {
            if (_albums.ContainsKey(id))
            {
                _albums.Remove(id);
                return true;
            }
            return false;
        }

        public AlbumModel? GetAlbumById(int id)
        {
            return _albums.ContainsKey(id) ? _albums[id] : null;
        }
    }
}
