using Microsoft.AspNetCore.Mvc;
using PhotoProject.Data.Base;
using PhotoProject.Data.ViewModels;
using PhotoProject.Models;

namespace PhotoProject.Data.Services
{
    public interface IPhotoService : IEntityBaseRepository<PhotoModel>
    {
        Task<PhotoModel> GetPhotoByIdAsync(int id);
        Task AddNewPhotoAsync(NewPhotoVM photo);
        Task<PhotoDropDownVM> GetPhotoDropDownValues();
        Task<bool> GetVoteAsync(string userId, int photoId);
        Task AddUpvoteAsync(PhotoModel photo);
        Task AddDownvoteAsync(PhotoModel photo);
    }
}
