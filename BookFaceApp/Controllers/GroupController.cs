using BookFaceApp.Core.Constants;
using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Group;
using BookFaceApp.Core.Services;
using BookFaceApp.Extensions;
using BookFaceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly IGroupService groupService;

        public GroupController(
            IGroupService _groupService)
        {
            groupService = _groupService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllGroupsQueryModel query)
{
            var result = await groupService.GetAllGroupsAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllGroupsQueryModel.GroupsPerPage);

            query.TotalGroupsCount = result.TotalGroupsCount;
            query.Categories = await groupService.GetCategoriesNamesAsync();
            query.Groups = result.Groups;

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new GroupAddModel()
            {
                Categories = await groupService.GetCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GroupAddModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories= await groupService.GetCategoriesAsync();

                return View(model);
            }

            if ((await groupService.CategoryExistsAsync(model.CategoryId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Category does not exist!";

                model.Categories = await groupService.GetCategoriesAsync();

                return View(model);
            }

            var userId = User.Id();

            await groupService.AddGroupAsync(model, userId!);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await groupService.GetOneGroupAsync(id);

            if (model == null)
            {
                TempData[MessageConstant.ErrorMessage] = "The group you are looking for was not found :(";

                return RedirectToAction(nameof(ErrorController.InvalidGroup), "Error");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await groupService.GetGroupForEditAsync(id);

            if (model == null)
            {
                TempData[MessageConstant.ErrorMessage] = "The group you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidGroup), "Error");
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
        public async Task<IActionResult> Edit(GroupEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await groupService.GetCategoriesAsync();

                return View(model);
            }

            if ((await groupService.ExistsByIdAsync(model.Id)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The group you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidGroup), "Error");
			}

            await groupService.EditGroupAsync(model);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
			if ((await groupService.ExistsByIdAsync(id)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The group you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidGroup), "Error");
			}

			var userId = User.Id();

			if ((await groupService.IsOwner(id, userId)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "You must be the owner in order to perform this action!";

				return RedirectToAction(nameof(ErrorController.NotOwner), "Error");
			}

            await groupService.DeleteGroupAsync(id);

			return RedirectToAction(nameof(All));
        }
    }
}
