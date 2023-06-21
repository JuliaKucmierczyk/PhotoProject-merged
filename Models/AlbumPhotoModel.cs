using PhotoProject.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace PhotoProject.Models
{
    public class AlbumPhotoModel : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public PhotoModel? Photo { get; set; }
        public int AlbumId { get; set; }
        public AlbumModel? Album { get; set; }
    }
}
