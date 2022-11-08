using BookFaceApp.Core.Constants;
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
            catch (ArgumentException ae)
            {
                if (ae.Message == "Invalid user ID")
                {
                    TempData[MessageConstant.ErrorMessage] = "Invalid user ID!";
                    return RedirectToAction("NotOwner", "Error");
                }

                if (ae.Message == "Invalid publication ID")
                {
                    TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";
                    return RedirectToAction("InvalidPublication", "Error");
                }

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await commentService.GetCommentForEditAsync(id);

            if (model == null)
            {
                TempData[MessageConstant.ErrorMessage] = "The comment you are looking for was not found :(";
                
                return RedirectToAction("InvalidPublication", "Error");
            }

            var userId = User.Id();

            if (model.UserId != userId)
            {
                TempData[MessageConstant.ErrorMessage] = "You need to be the owner in order to perform this action!";

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

            int id = model.Publicationid;

            return RedirectToAction("Details", "Publication", new { id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.Id();

                await commentService.DeleteCommentAsync(id, userId!);
            }
            catch (ArgumentException ae)
            {
                if (ae.Message == "Invalid user ID")
                {
                    TempData[MessageConstant.ErrorMessage] = "Invalid user ID!";
                    return RedirectToAction("NotOwner", "Error");
                }

                if (ae.Message == "Invalid comment ID")
                {
                    TempData[MessageConstant.ErrorMessage] = "The comment you are looking for was not found :(";
                    return RedirectToAction("InvalidComment", "Error");
                }

                if (ae.Message == "Invalid owner ID")
                {
                    TempData[MessageConstant.ErrorMessage] = "You need to be the owner in order to perform this action!";
                    return RedirectToAction("NotOwner", "Error");
                }
            }

            return RedirectToAction("All", "Publication");
        }
    }
}
