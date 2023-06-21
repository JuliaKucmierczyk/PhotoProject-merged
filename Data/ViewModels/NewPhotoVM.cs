using PhotoProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoProject.Data.ViewModels
{
    public class NewPhotoVM
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }
        public string Tags { get; set; }
        public string? Camera { get; set; }
        public string? Details { get; set; }
        public AccessLevel Access { get; set; }
        //public string ImageURL { get; set; }
        public IFormFile ImageFile { get; set; }

        // relacja zdjecie --> komentarze
        public List<CommentModel>? Comments { get; set; }

        //relationships
        public List<int>? AlbumsId { get; set; }
        public int AuthorId { get; set; }
    }
}
