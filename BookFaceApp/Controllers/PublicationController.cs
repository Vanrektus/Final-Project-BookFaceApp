using BookFaceApp.Core.Constants;
using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Extensions;
using BookFaceApp.Core.Models.Publication;
using BookFaceApp.Extensions;
using BookFaceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookFaceApp.Controllers.Constants.ControllersConstants.RolesNamesConstants;
using static BookFaceApp.Controllers.Constants.ControllersConstants.ControllersNamesConstants;

namespace BookFaceApp.Controllers
{
    [Authorize]
    public class PublicationController : Controller
    {
        private readonly IPublicationService publicationService;
        private readonly IGroupService groupService;

        public PublicationController(
            IPublicationService _publicationService,
            IGroupService _groupService)
        {
            publicationService = _publicationService;
            groupService = _groupService;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllPublicationsQueryModel query)
        {
            var result = await publicationService.GetAllPublicationsAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllPublicationsQueryModel.PublicationsPerPage);

            query.TotalPublicationsCount = result.TotalPublicationsCount;
            query.Categories = await publicationService.GetCategoriesNamesAsync();
            query.Publications = result.Publications;

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var model = new PublicationAddModel()
            {
                Categories = await publicationService.GetCategoriesAsync()
            };

            if (id != 0)
            {
                model.GroupId = id;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PublicationAddModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await publicationService.GetCategoriesAsync();

                return View(model);
            }

            if ((await publicationService.CategoryExistsAsync(model.CategoryId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Category does not exist!";

                model.Categories = await publicationService.GetCategoriesAsync();

                return View(model);
            }

            if (model.GroupId != null)
            {
                if ((await groupService.ExistsByIdAsync(model.GroupId)) == false)
                {
                    TempData[MessageConstant.ErrorMessage] = "The group, you are trying to add this publication to, does not exist!";

					model.Categories = await publicationService.GetCategoriesAsync();

					return View(model);
				}

                var groupCategoryId = await groupService.GetCategoryIdAsync(model.GroupId);

                if (publicationService.PublicationCatMatchesGroupCat(model.CategoryId, groupCategoryId) == false)
                {
                    TempData[MessageConstant.ErrorMessage] = "Publication category must match the group category.";

                    model.Categories = await publicationService.GetCategoriesAsync();

                    return View(model);
                }
            }

            var userId = User.Id();

            int id = await publicationService.AddPublicationAsync(model, userId!);

            if (model.GroupId != null)
            {
                int? ID = model.GroupId;

                return RedirectToAction(nameof(GroupController.Details), GroupControllerName, new { ID });
            }

            return RedirectToAction(nameof(Details), new { id = id, information = model.GetInformation() });
        }
         
        [HttpGet]
        public async Task<IActionResult> Details(int id, string information)
        {
			if ((await publicationService.ExistsAsync(id)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidPublication), ErrorControllerName);
			}

			var model = await publicationService.GetOnePublicationAsync(id);

            if (information != null && information != model.GetInformation())
            {
                TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";

                return RedirectToAction(nameof(ErrorController.InvalidPublication), ErrorControllerName);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
			if ((await publicationService.ExistsAsync(id)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidPublication), ErrorControllerName);
			}

			var model = await publicationService.GetPublicationForEditAsync(id);

			var userId = User.Id();

			if ((await publicationService.IsOwnerAsync(model.Id, userId)) == false
                && User.IsInRole(Admin) == false
                && ((await publicationService.IsInGroupAsync(model.Id)) && (await groupService.IsOwnerAsync((int)model.GroupId!, userId))) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "You must be the owner in order to perform this action!";

				return RedirectToAction(nameof(ErrorController.NotOwner), ErrorControllerName);
			}

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PublicationEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await publicationService.GetCategoriesAsync();

                return View(model);
            }

            if ((await publicationService.ExistsAsync(model.Id)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidPublication), ErrorControllerName);
			}

			if (model.GroupId != null)
			{
				if ((await groupService.ExistsByIdAsync(model.GroupId)) == false)
				{
					TempData[MessageConstant.ErrorMessage] = "The group, you are trying to add this publication to, does not exist!";

					model.Categories = await publicationService.GetCategoriesAsync();

					return View(model);
				}

				var groupCategoryId = await groupService.GetCategoryIdAsync(model.GroupId);

				if (publicationService.PublicationCatMatchesGroupCat(model.CategoryId, groupCategoryId) == false)
				{
					TempData[MessageConstant.ErrorMessage] = "Publication category must match the group category.";

					model.Categories = await publicationService.GetCategoriesAsync();

					return View(model);
				}
			}

			await publicationService.EditPublicationAsync(model);

            return RedirectToAction(nameof(Details), new { id = model.Id, information = model.GetInformation() });
        }

        [HttpPost]
        public async Task<IActionResult> LikePublication(int id)
        {
            if ((await publicationService.ExistsAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";

                return RedirectToAction(nameof(ErrorController.InvalidPublication), ErrorControllerName);
            }

            var userId = User.Id();

            await publicationService.LikePublicationAsync(id, userId!);

            //return RedirectToAction(nameof(StatisticsApiController), "StatisticsApi", new { id });

            return RedirectToAction(nameof(All));

        }

        public async Task<IActionResult> Delete(int id)
        {
            if ((await publicationService.ExistsAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";

                return RedirectToAction(nameof(ErrorController.InvalidPublication), ErrorControllerName);
            }

            var userId = User.Id();

            if ((await publicationService.IsOwnerAsync(id, userId)) == false
                && User.IsInRole(Admin) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "You must be the owner in order to perform this action!";

                return RedirectToAction(nameof(ErrorController.NotOwner), ErrorControllerName);
            }

            await publicationService.DeletePublicationAsync(id);

            if (await publicationService.IsInGroupAsync(id))
			{
                int ID = await publicationService.GetPublicationGroupIdAsync(id);

				return RedirectToAction(nameof(GroupController.Details), GroupControllerName, new { ID });
			}

            return RedirectToAction(nameof(All));
        }
    }
}
