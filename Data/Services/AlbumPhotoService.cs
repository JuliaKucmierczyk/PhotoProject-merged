using Microsoft.EntityFrameworkCore;
using PhotoProject.Data.Base;
using PhotoProject.Data.ViewModels;
using PhotoProject.Models;

namespace PhotoProject.Data.Services
{
    public class AlbumPhotoService : EntityBaseRepository<AlbumPhotoModel>, IAlbumPhotoService
    {
        private readonly AppDbContext _context;
        public AlbumPhotoService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<AlbumPhotoModel> GetAlbumPhotoByIdAsync(int id)
        {
            var details = _context.AlbumPhotos.Include(a => a.Album).Include(p => p.Photo).FirstOrDefaultAsync(n => n.Id == id);
            return await details;
        }
    }
}