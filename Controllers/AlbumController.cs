using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoProject.Data.Services;
using PhotoProject.Data.ViewModels;
using PhotoProject.Models;

namespace PhotoProject.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _service;

        public AlbumController(IAlbumService service)
        {
            _service = service;
        }

        // Details for a photo
        [AllowAnonymous]
        public async Task<IActionResult> Photos(int id)
        {
            var albumPhotos = await _service.GetAlbumByIdAsync(id);

            return View(albumPhotos);
        }

        // Show all albums
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }


        //Add new album
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var dropdown = await _service.GetPhotoDropDownValues();
                string currentUserId = User.Identity.GetUserId();

                ViewBag.PhotosId = new SelectList(dropdown.Photos.Where(x => x.AuthorId == currentUserId), "Id", "Name");
                ViewBag.User = currentUserId;

                return View();
            }
            else { return View("Empty"); }
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(NewAlbumVM album)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    await Create();
                    return RedirectToAction("Create");

                }
                else
                {
                    await _service.AddNewAlbumAsync(album);
                    return RedirectToAction("Index");
                }
            }
            else { return View("Empty"); }
        }

        // move photos
        public async Task<IActionResult> Move(int id)
        {
            var albumDetails = await _service.GetAlbumByIdAsync(id);
            string userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() == albumDetails.AuthorId || User.IsInRole("ADMIN"))
            {

                string currentUserId = User.Identity.GetUserId();
                var dropdownAlbum = await _service.GetAlbumDropDownValues();
                var dropdownPhoto = await _service.GetPhotoDropDownValues();

                ViewBag.AuthorId = currentUserId;
                ViewBag.CurrentAlbumId = id;
                ViewBag.AlbumsId = new SelectList(dropdownAlbum.Albums, "Id", "Name");
                ViewBag.PhotosId = new SelectList(dropdownPhoto.Photos, "Id", "Name");
                return View("Move");
            }
            else { return View("Empty"); }
        }
        public async Task<IActionResult> Tescik(int currentAlbumId, int selectedAlbumId, List<int> selectedPhotosId)
        {

            string userId = User.Identity.GetUserId();
            var albumDetails = await _service.GetAlbumByIdAsync(currentAlbumId);
            if (User.Identity.GetUserId() == albumDetails.AuthorId || (User.IsInRole("ADMIN")))
            {
                var currAlbum = currentAlbumId;
                var desAlbum = selectedAlbumId;
                List<int> selPhotos = selectedPhotosId;

                var CurrAlbumDetails = await _service.GetAlbumByIdAsync(currAlbum);
                var DesAlbumDetails = await _service.GetAlbumByIdAsync(desAlbum);

                foreach (var photoId in selPhotos)
                {
                    var albumPhoto = await _service.GetAlbumPhotoByBothIdAsync(currAlbum, photoId);

                    int test = albumPhoto.Id;
                    await _service.AddAsyncMove(photoId, desAlbum);
                    int deleteId = albumPhoto.Id;
                    await _service.DeleteAlbumPhotoAsync(albumPhoto);
                }
                return RedirectToAction("Index");
            }
            else { return View("Empty"); }

        }


        // Update album
        public async Task<IActionResult> Update(int id)
        {
            var albumDetails = await _service.GetAlbumByIdAsync(id);
            string userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() == albumDetails.AuthorId || (User.IsInRole("ADMIN")))
            {
                var dropdown = await _service.GetPhotoDropDownValues();

                ViewBag.AuthorId = new SelectList(dropdown.Albums, "Id", "Name");
                ViewBag.PhotosId = new SelectList(dropdown.Photos, "Id", "Name");



                var photoDetails = await _service.GetByIdAsync(id);

                if (photoDetails == null)
                    return View("Empty");
                return View(photoDetails);
            }
            else { return View("Empty"); }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(int id, AlbumModel album)
        {
            string userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() == album.AuthorId || (User.IsInRole("ADMIN")))
            {
                if (!ModelState.IsValid)
                {
                    return View("Empty");
                }
                await _service.UpdateAsync(id, album);
                return RedirectToAction("Index");
            }
            else { return View("Empty"); }
        }

        // Delete album
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _service.GetAlbumByIdAsync(id);
            string userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() == album.AuthorId || (User.IsInRole("ADMIN")))
            {
                var albumDetails = await _service.GetByIdAsync(id);
                if (albumDetails == null)
                    return View("Empty");
                return View(albumDetails);
            }
            else { return View("Empty"); }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _service.GetAlbumByIdAsync(id);
            string userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() == album.AuthorId || (User.IsInRole("ADMIN")))
            {
                var albumDetails = await _service.GetByIdAsync(id);
                if (albumDetails == null) return View("Empty");


                await _service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            else { return View("Empty"); }
        }
        public async Task<IActionResult> DeleteConfirmedAp(int id, int id2)
        {
            var album = await _service.GetAlbumByIdAsync(id);
            string userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() == album.AuthorId || (User.IsInRole("ADMIN")))
            {
                var albumPhotoDetails = await _service.GetAlbumPhotoByBothIdAsync(id, id2);
                if (albumPhotoDetails == null) return View("Empty");

                await _service.DeleteAlbumPhotoAsync(albumPhotoDetails);
                return RedirectToAction("Index");
            }
            else { return View("Empty"); }
        }


    }
}
