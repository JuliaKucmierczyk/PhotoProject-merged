using PhotoProject.Data.Base;
using PhotoProject.Models;
using System.ComponentModel.DataAnnotations;

namespace PhotoProject.Data.ViewModels
{
    public class NewAlbumVM
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }
        public string AuthorId { get; set; }

        public AccessLevel Access { get; set; }
        public string? ImageURL { get; set; }

        public IFormFile ImageFile { get; set; }


        // relacja zdjecia <--> album,  wiele do wielu
        public List<AlbumPhotoModel>? AlbumsPhotos { get; set; }

        //relationships
        public List<int>? PhotosId { get; set; }

    }
}
