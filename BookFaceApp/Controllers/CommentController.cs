using BookFaceApp.Contracts;
using BookFaceApp.Models.Comment;
using BookFaceApp.Models.Publication;
using BookFaceApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookFaceApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(
            ICommentService _commentService)
        {
            commentService = _commentService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new CommentAddModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentAddModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                await commentService.AddCommentAsync(model, id, userId!);

                return RedirectToAction("All", "Publication");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await commentService.GetCommentForEditAsync(id);

            if (model == null)
            {
                return RedirectToAction("InvalidPublication", "Error");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (model.UserId != userId)
            {
                return RedirectToAction("NotOwner", "Error");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CommentEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await commentService.EditCommentAsync(model);

            return RedirectToAction("All", "Publication");
        }
    }
}
