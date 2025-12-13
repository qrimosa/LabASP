using Lab0.Models;

namespace Lab0.Models
{
    public class AlbumMemoryService : IAlbumService
    {
        private readonly Dictionary<int, Label> _labels = new()
        {
            { 1, new Label { Id = 1, Name = "Warner Bros. Records", Country = "USA" } },
            { 2, new Label { Id = 2, Name = "DGC Records", Country = "USA" } },
            { 3, new Label { Id = 3, Name = "EMI", Country = "UK" } },
            { 4, new Label { Id = 4, Name = "Sony Music", Country = "Japan" } }
        };

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
                    TotalDuration = new TimeSpan(0, 37, 45),
                    LabelId = 1
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
                    TotalDuration = new TimeSpan(0, 49, 23),
                    LabelId = 2
                }
            },
            {
                3,
                new AlbumModel()
                {
                    Id = 3,
                    Name = "Meteora",
                    Band = "Linkin Park",
                    Songs = new List<string> { "Numb", "Somewhere I Belong" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(2003, 3, 25),
                    TotalDuration = new TimeSpan(0, 36, 35),
                    LabelId = 1
                }
            },
            {
                4,
                new AlbumModel()
                {
                    Id = 4,
                    Name = "Back in Black",
                    Band = "AC/DC",
                    Songs = new List<string> { "Hells Bells", "Back in Black" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1980, 7, 25),
                    TotalDuration = new TimeSpan(0, 42, 11),
                    LabelId = 3
                }
            },
            {
                5,
                new AlbumModel()
                {
                    Id = 5,
                    Name = "OK Computer",
                    Band = "Radiohead",
                    Songs = new List<string> { "Paranoid Android", "Karma Police" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1997, 5, 21),
                    TotalDuration = new TimeSpan(0, 53, 27),
                    LabelId = 3
                }
            },
            {
                6,
                new AlbumModel()
                {
                    Id = 6,
                    Name = "Californication",
                    Band = "Red Hot Chili Peppers",
                    Songs = new List<string> { "Scar Tissue", "Otherside" },
                    ChartPosition = 3,
                    ReleaseDate = new DateOnly(1999, 6, 8),
                    TotalDuration = new TimeSpan(0, 56, 24),
                    LabelId = 4
                }
            },
            {
                7,
                new AlbumModel()
                {
                    Id = 7,
                    Name = "The Dark Side of the Moon",
                    Band = "Pink Floyd",
                    Songs = new List<string> { "Time", "Money" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1973, 3, 1),
                    TotalDuration = new TimeSpan(0, 42, 49),
                    LabelId = 3
                }
            },
            {
                8,
                new AlbumModel()
                {
                    Id = 8,
                    Name = "Abbey Road",
                    Band = "The Beatles",
                    Songs = new List<string> { "Come Together", "Something" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1969, 9, 26),
                    TotalDuration = new TimeSpan(0, 47, 23),
                    LabelId = 3
                }
            },
            {
                9,
                new AlbumModel()
                {
                    Id = 9,
                    Name = "Thriller",
                    Band = "Michael Jackson",
                    Songs = new List<string> { "Beat It", "Billie Jean" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1982, 11, 30),
                    TotalDuration = new TimeSpan(0, 42, 19),
                    LabelId = 4
                }
            },
            {
                10,
                new AlbumModel()
                {
                    Id = 10,
                    Name = "Led Zeppelin IV",
                    Band = "Led Zeppelin",
                    Songs = new List<string> { "Black Dog", "Stairway to Heaven" },
                    ChartPosition = 2,
                    ReleaseDate = new DateOnly(1971, 11, 8),
                    TotalDuration = new TimeSpan(0, 42, 40),
                    LabelId = 3
                }
            }
        };

        private int currentId = 10;

        public IQueryable<AlbumModel> GetAlbumsQuery()
        {
            foreach (var album in _albums.Values)
            {
                if (album.LabelId.HasValue && _labels.TryGetValue(album.LabelId.Value, out var label))
                {
                    album.Label = label;
                }
            }

            return _albums.Values.AsQueryable();
        }

        public List<AlbumModel> GetAlbums()
        {
            return GetAlbumsQuery().ToList();
        }

        public void AddAlbum(AlbumModel album)
        {
            album.Id = ++currentId;
            if (album.LabelId.HasValue && _labels.TryGetValue(album.LabelId.Value, out var label))
            {
                album.Label = label;
            }
            _albums.Add(album.Id, album);
        }

        public bool UpdateAlbum(AlbumModel album)
        {
            if (_albums.ContainsKey(album.Id))
            {
                if (album.LabelId.HasValue && _labels.TryGetValue(album.LabelId.Value, out var label))
                {
                    album.Label = label;
                }
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
            if (_albums.TryGetValue(id, out var album))
            {
                if (album.LabelId.HasValue && _labels.TryGetValue(album.LabelId.Value, out var label))
                {
                    album.Label = label;
                }
                return album;
            }

            return null;
        }
    }
}
