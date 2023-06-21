using PhotoProject.Areas.Identity.Data;
using PhotoProject.Data;
using PhotoProject.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoProject.Models
{
    public class RateModel : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public bool Rate { get; set; } = false;
        public string AuthorId { get; set; }

        public int PhotoId { get; set; }
        [ForeignKey("PhotoId")]
        public PhotoModel? Photo { get; set; }

    }
}
