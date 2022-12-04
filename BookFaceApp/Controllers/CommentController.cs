using BookFaceApp.Core.Constants;
using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Comment;
using BookFaceApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookFaceApp.Controllers.Constants.ControllersConstants.ControllersNamesConstants;
using static BookFaceApp.Controllers.Constants.ControllersConstants.RolesNamesConstants;

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

				return RedirectToAction(nameof(ErrorController.InvalidPublication), ErrorControllerName);
			}

			var userId = User.Id();

			await commentService.AddCommentAsync(model, id, userId!);

			return RedirectToAction(nameof(PublicationController.Details), PublicationControllerName, new { id });
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			if ((await commentService.ExistsAsync(id)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The comment you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidComment), ErrorControllerName);
			}

			var model = await commentService.GetCommentForEditAsync(id);

			var userId = User.Id();

			if ((await commentService.IsOwnerAsync(model.Id, userId)) == false
				&& User.IsInRole(Admin) == false
				&& (await publicationService.IsOwnerAsync(model.Publicationid, userId)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "You need to be the owner in order to perform this action!";

				return RedirectToAction(nameof(ErrorController.NotOwner), ErrorControllerName);
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

				return RedirectToAction(nameof(ErrorController.InvalidComment), ErrorControllerName);
			}

			await commentService.EditCommentAsync(model);

			int id = model.Publicationid;

			return RedirectToAction(nameof(PublicationController.Details), PublicationControllerName, new { id });
		}

		public async Task<IActionResult> Delete(int id)
		{
			if ((await commentService.ExistsAsync(id)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The comment you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidComment), ErrorControllerName);
			}

			var publicationId = await commentService.GetPublicationIdByCommentIdAsync(id);
			var userId = User.Id();

			if ((await commentService.IsOwnerAsync(id, userId)) == false
				&& (await publicationService.IsOwnerAsync(publicationId, userId)) == false
				&& User.IsInRole(Admin) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "You need to be the owner in order to perform this action!";

				return RedirectToAction(nameof(ErrorController.NotOwner), ErrorControllerName);
			}

			var ID = await commentService.GetPublicationIdByCommentIdAsync(id);

			await commentService.DeleteCommentAsync(id);

			return RedirectToAction(nameof(PublicationController.Details), PublicationControllerName, new { ID });
		}
	}
}
