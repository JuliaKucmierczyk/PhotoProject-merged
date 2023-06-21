using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PhotoProject.Areas.Identity.Data;
using PhotoProject.Data;
using PhotoProject.Data.Services;
using PhotoProject.Data.ViewModels;
using PhotoProject.Models;
using System.Security.Claims;

namespace PhotoProject.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoService _service;

        public PhotoController(IPhotoService service)
        {
            _service = service;
        }

        // Details for a photo
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var photoDetails = await _service.GetPhotoByIdAsync(id);
            return View(photoDetails);
        }

        // Show all photos
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SearchPhoto(string searchPhoto)
        {
            var data = await _service.GetAllAsync();


            if (!string.IsNullOrEmpty(searchPhoto))
            {
                var dataFiltered = data.Where(n => n.Name.IndexOf(searchPhoto, StringComparison.OrdinalIgnoreCase) != -1);
                return View("Index", dataFiltered);
            }
            return View("Index", data);

        }


        //Add new photo
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var dropdown = await _service.GetPhotoDropDownValues();
                string currentUserId = User.Identity.GetUserId();
                ViewBag.User = currentUserId;


                ViewBag.AlbumId = new SelectList(dropdown.Albums.Where(x => x.AuthorId == currentUserId), "Id", "Name");


                return View();
            }
            else { return View("Empty"); }
        }

        public async Task<IActionResult> ShowNativeResolution(int id)
        {
            var photoDetails = await _service.GetByIdAsync(id);

            var memory = ShowNativeResolutionStream(photoDetails.ImageName, "wwwroot\\images");
            return File(memory.ToArray(), "image/png");


        }

        private MemoryStream ShowNativeResolutionStream(string filename, string uploadPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, filename);
            var memory = new MemoryStream();
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(NewPhotoVM photo)
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
                    await _service.AddNewPhotoAsync(photo);
                    return RedirectToAction("Index");
                }
            }
            else { return View("Empty"); }
        }

        // Update photo
        public async Task<IActionResult> Update(int id)
        {
            var photo = await _service.GetByIdAsync(id);
            string userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() == photo.AuthorId || (User.IsInRole("ADMIN")))
            {
                var dropdown = await _service.GetPhotoDropDownValues();
                ViewBag.AuthorId = new SelectList(dropdown.Albums, "Id", "Name");


                var photoDetails = await _service.GetByIdAsync(id);

                if (photoDetails == null)
                    return View("Empty");
                return View(photoDetails);
            }
            else { return View("Empty"); }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(int id, PhotoModel photo)
        {
            var p = await _service.GetPhotoByIdAsync(id);
            string userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() == p.AuthorId || (User.IsInRole("ADMIN")))
            {
                {
                    if (!ModelState.IsValid)
                    {
                        return View("Empty");
                    }
                    await _service.UpdateAsync(id, photo);
                    return RedirectToAction("Index");
                }
            }
            else { return View("Empty"); }
        }

        // Delete photo
        public async Task<IActionResult> Delete(int id)
        {
            var photo = await _service.GetByIdAsync(id);
            string userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() == photo.AuthorId || (User.IsInRole("ADMIN")))
            {
                var photoDetails = await _service.GetByIdAsync(id);
                if (photoDetails == null)
                    return View("Empty");
                return View(photoDetails);
            }
            else { return View("Empty"); }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photo = await _service.GetByIdAsync(id);
            string userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() == photo.AuthorId || (User.IsInRole("ADMIN")))
            {
                var photoDetails = await _service.GetByIdAsync(id);
                if (photoDetails == null) return View("Empty");


                await _service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            else { return View("Empty"); }
        }
        public async Task<IActionResult> AddComment(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["photoId"] = id;
                return RedirectToAction("Create", "Comment");
            }
            else { return View("Empty"); }
        }

        public async Task<IActionResult> Upvote(int id)
        {
            var photo = await _service.GetByIdAsync(id);
            if (User.Identity.IsAuthenticated && User.Identity.GetUserId() != photo.AuthorId)
            {
                string currentUserId = User.Identity.GetUserId();
                var voteDetails = await _service.GetVoteAsync(currentUserId, id);

                if (voteDetails == true)
                {
                    var photoDetails = await _service.GetByIdAsync(id);
                    await _service.AddUpvoteAsync(photoDetails);
                    return RedirectToAction("Details", "Photo", new { id = id });
                }
                else
                {
                    return RedirectToAction("Details", "Photo", new { id = id });
                }
            }
            else { return View("Empty"); }
        }

        public async Task<IActionResult> Downvote(int id)
        {
            var photo = await _service.GetByIdAsync(id);
            if (User.Identity.IsAuthenticated && User.Identity.GetUserId() != photo.AuthorId)
            {
                string currentUserId = User.Identity.GetUserId();
                var voteDetails = await _service.GetVoteAsync(currentUserId, id);

                if (voteDetails == true)
                {
                    var photoDetails = await _service.GetByIdAsync(id);
                    await _service.AddDownvoteAsync(photoDetails);
                    return RedirectToAction("Details", "Photo", new { id = id });
                }
                else
                {
                    return RedirectToAction("Details", "Photo", new { id = id });
                }
            }
            else { return View("Empty"); }
        }



    }
}
