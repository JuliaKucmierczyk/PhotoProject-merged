using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PhotoProject.Controllers;
using PhotoProject.Data.Base;
using PhotoProject.Data.Services;
using PhotoProject.Data.ViewModels;
using PhotoProject.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace PhotoProject.Data.Services
{
    public class PhotoService : EntityBaseRepository<PhotoModel>, IPhotoService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PhotoService(AppDbContext context, IWebHostEnvironment hostEnvironment) : base(context)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public async Task AddNewPhotoAsync(NewPhotoVM photo)
        {
            var newPhoto = new PhotoModel()
            {
                AuthorId = photo.AuthorId,
                Name = photo.Name,
                Tags = photo.Tags,
                Camera = photo.Camera,
                Details = photo.Details,
                Access = photo.Access,
                ImageFile = photo.ImageFile,
                ImageName = photo.ImageName
            };
            // save image
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileName(newPhoto.ImageFile.FileName);
            string extension = Path.GetExtension(newPhoto.ImageFile.FileName);
            newPhoto.ImageName = fileName;
            string path = Path.Combine(wwwRootPath + "/images/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await newPhoto.ImageFile.CopyToAsync(fileStream);
            }
            await _context.AddAsync(newPhoto);
            await _context.SaveChangesAsync();

            if (photo.AlbumsId != null)
            {
                foreach (var albumId in photo.AlbumsId)
                {
                    var newPhotoAlbum = new AlbumPhotoModel()
                    {
                        PhotoId = newPhoto.Id,
                        AlbumId = albumId
                    };
                    await _context.AlbumPhotos.AddAsync(newPhotoAlbum);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PhotoModel> GetPhotoByIdAsync(int id)
        {
            var photoDetails = _context.Photos.Include(a => a.Author).Include(cm => cm.Comments).ThenInclude(ac => ac.Author).Include(ap => ap.AlbumsPhotos).ThenInclude(c => c.Album).FirstOrDefaultAsync(n => n.Id == id);
            return await photoDetails;
        }

        public async Task<PhotoDropDownVM> GetPhotoDropDownValues()
        {
            var dropdown = new PhotoDropDownVM()
            {
                Albums = await _context.Albums.OrderBy(n => n.Name).ToListAsync()
            };
            return dropdown;
        }


        public async Task AddAsyncMove(int photoId, int albumId)
        {

            var newAlbumPhoto = new AlbumPhotoModel()
            {
                AlbumId = albumId,
                PhotoId = photoId,
            };
            await _context.AlbumPhotos.AddAsync(newAlbumPhoto);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> GetVoteAsync(string userId, int photoId)
        {
            var vote = _context.Rates.Where(x => x.AuthorId == userId && x.PhotoId == photoId).FirstOrDefault();
            if (vote == null)
            {
                var newVote = new RateModel()
                {
                    AuthorId = userId,
                    PhotoId = photoId,
                };
                await _context.Rates.AddAsync(newVote);
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }


        }

        public async Task AddUpvoteAsync(PhotoModel photo)
        {

            var photoDetails = photo;
            photoDetails.UpVote++;
            await _context.SaveChangesAsync();
            int num = photoDetails.UpVote;
            num = photoDetails.UpVote;
        }
        public async Task AddDownvoteAsync(PhotoModel photo)
        {
            var photoDetails = photo;
            photoDetails.DownVote++;
            await _context.SaveChangesAsync();
            int num = photoDetails.UpVote;
            num = photoDetails.UpVote;
        }
    }
}