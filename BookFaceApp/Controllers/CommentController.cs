using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Comment;
using BookFaceApp.Extensions;
using Microsoft.AspNetCore.Mvc;

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
                var userId = User.Id();

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

            var userId = User.Id();

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

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.Id();

                await commentService.DeleteCommentAsync(id, userId!);
            }
            catch (Exception)
            {
                throw;
                //return RedirectToAction("NotOwner", "Error");
            }

            return RedirectToAction("All", "Publication");
        }
    }
}
