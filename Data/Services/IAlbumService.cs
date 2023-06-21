using Microsoft.AspNetCore.Mvc;
using PhotoProject.Data.Base;
using PhotoProject.Data.ViewModels;
using PhotoProject.Models;

namespace PhotoProject.Data.Services
{
    public interface IAlbumService : IEntityBaseRepository<AlbumModel>
    {
        Task<AlbumModel> GetAlbumByIdAsync(int id);
        Task AddNewAlbumAsync(NewAlbumVM album);
        Task<PhotoDropDownVM> GetPhotoDropDownValues();
        Task<AlbumPhotoModel> GetAlbumPhotoByBothIdAsync(int albumId, int photoId);
        Task AddAsyncMove(int photoId, int albumId);
        Task<PhotoDropDownVM> GetAlbumDropDownValues();
        Task DeleteAlbumPhotoAsync(AlbumPhotoModel ap);
    }
}
