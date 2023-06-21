using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoProject.Data.Services;
using PhotoProject.Models;

namespace PhotoProject.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        //Add new comment
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                string currentUserId = User.Identity.GetUserId();
                ViewBag.User = currentUserId;
                ViewBag.Photo = TempData["photoId"];
                return View();
            }
            else { return View("Empty"); }
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(CommentModel comment)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return View("Create");
                }
                await _service.AddNewCommentAsync(comment);
                return RedirectToAction("Details", "Photo", new { id = comment.PhotoId });
            }
            else { return View("Empty"); }
        }




    }
}
