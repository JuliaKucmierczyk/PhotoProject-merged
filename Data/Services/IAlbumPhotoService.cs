using PhotoProject.Data.Base;
using PhotoProject.Models;

namespace PhotoProject.Data.Services
{
    public interface IAlbumPhotoService : IEntityBaseRepository<AlbumPhotoModel>
    {
        Task<AlbumPhotoModel> GetAlbumPhotoByIdAsync(int id);
    }
}
