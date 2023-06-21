using System.ComponentModel.DataAnnotations;

namespace PhotoProject.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // relationships
        public List<PhotoModel>? Photos { get; set; }
        public List<CommentModel>? Comments { get; set; }
    }
}
