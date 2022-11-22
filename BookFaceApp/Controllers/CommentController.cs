using BookFaceApp.Core.Constants;
using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Comment;
using BookFaceApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
	[Authorize]
	public class CommentController : Controller
	{
		private readonly ICommentService commentService;
		private readonly IPublicationService publicationService;

		public CommentController(
			ICommentService _commentService,
			IPublicationService _publicationService)
		{
			commentService = _commentService;
			publicationService = _publicationService;
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

			if ((await publicationService.ExistsAsync(id)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidPublication), "Error");
			}

			var userId = User.Id();

			await commentService.AddCommentAsync(model, id, userId!);

			return RedirectToAction(nameof(PublicationController.Details), "Publication", new { id });
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var model = await commentService.GetCommentForEditAsync(id);

			if (model == null)
			{
				TempData[MessageConstant.ErrorMessage] = "The comment you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidComment), "Error");
			}

			var userId = User.Id();

			if (model.UserId != userId)
			{
				TempData[MessageConstant.ErrorMessage] = "You need to be the owner in order to perform this action!";

				return RedirectToAction(nameof(ErrorController.NotOwner), "Error");
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

			if ((await commentService.ExistsAsync(model.Id)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The comment you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidComment), "Error");
			}

			await commentService.EditCommentAsync(model);

			int id = model.Publicationid;

			return RedirectToAction(nameof(PublicationController.Details), "Publication", new { id });
		}

		public async Task<IActionResult> Delete(int id)
		{
			if ((await commentService.ExistsAsync(id)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The comment you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidComment), "Error");
			}

			var userId = User.Id();

			if (( await commentService.IsOwner(id, userId)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "You need to be the owner in order to perform this action!";

				return RedirectToAction(nameof(ErrorController.NotOwner), "Error");
			}

			await commentService.DeleteCommentAsync(id);

			return RedirectToAction(nameof(PublicationController.Details), "Publication", new { id });
		}
	}
}
