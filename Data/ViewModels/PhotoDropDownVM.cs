using PhotoProject.Models;

namespace PhotoProject.Data.ViewModels
{
    public class PhotoDropDownVM
    {
        public PhotoDropDownVM()
        {
            Albums = new List<AlbumModel>();
            Photos = new List<PhotoModel>();
        }
        public List<AlbumModel> Albums { get; set; }
        public List<PhotoModel> Photos { get; set; }
    }
}
