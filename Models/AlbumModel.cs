using PhotoProject.Areas.Identity.Data;
using PhotoProject.Data;
using PhotoProject.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoProject.Models
{
    public class AlbumModel : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }
        public string AuthorId { get; set; }
        public AccessLevel Access { get; set; }
        public string? ImageURL { get; set; }
        [NotMapped]
        [DisplayName("Upload Photo")]
        public IFormFile? ImageFile { get; set; }

        // relationship photos >--< albums
        public List<AlbumPhotoModel>? AlbumsPhotos { get; set; }

    }
}
