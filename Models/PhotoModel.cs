using PhotoProject.Areas.Identity.Data;
using PhotoProject.Data;
using PhotoProject.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoProject.Models
{
    public class PhotoModel : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }

        public string Tags { get; set; }
        public string? Camera { get; set; }
        public string? Details { get; set; }
        public AccessLevel Access { get; set; }
        public string? ImageURL { get; set; }
        public bool Selected { get; set; }
        [NotMapped]
        [DisplayName("Upload Photo")]
        public IFormFile? ImageFile { get; set; }

        public int UpVote { get; set; }
        public int DownVote { get; set; }

        //--------------------------------  
        public RateModel? Rates { get; set; }
        //relacja zdjecie --> komentarze
        public List<CommentModel>? Comments { get; set; }

        // relationship photos >--< albums 
        public List<AlbumPhotoModel>? AlbumsPhotos { get; set; }

        // relationship author <--> photo
        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public PhotoProjectUser? Author { get; set; }
    }
}
