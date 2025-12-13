namespace Lab0.Models
{
    public interface IAlbumService
    {
        IQueryable<AlbumModel> GetAlbumsQuery();

        List<AlbumModel> GetAlbums();

        void AddAlbum(AlbumModel album);
        bool UpdateAlbum(AlbumModel album);
        bool DeleteAlbumById(int id);
        AlbumModel? GetAlbumById(int id);
    }
}