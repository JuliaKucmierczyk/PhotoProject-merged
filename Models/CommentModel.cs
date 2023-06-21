using PhotoProject.Areas.Identity.Data;
using PhotoProject.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoProject.Models
{
    public class CommentModel : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Comments { get; set; }


        // relationship photo --> comment
        public int? PhotoId { get; set; }
        [ForeignKey("PhotoId")]
        public PhotoModel? Photo { get; set; }


        public string? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public PhotoProjectUser? Author { get; set; }
    }
}
