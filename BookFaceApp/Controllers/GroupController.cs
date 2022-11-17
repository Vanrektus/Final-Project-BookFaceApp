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
        public async Task<IActionResult> AllOLD()
        {
            var model = await groupService.GetAllGroupsAsync();

            return View(model);
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
                return View(model);
            }

            try
            {
                var userId = User.Id();

                await groupService.AddGroupAsync(model, userId!);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await groupService.GetOneGroupAsync(id);

            if (model != null)
            {
                return View(model);
            }

            TempData[MessageConstant.ErrorMessage] = "The group you are looking for was not found :(";

            return RedirectToAction(nameof(ErrorController.InvalidPublication), "Error");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await groupService.GetGroupForEditAsync(id);

            if (model == null)
            {
                TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";

                return RedirectToAction("InvalidPublication", "Error");
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
                return View(model);
            }

            await groupService.EditGroupAsync(model);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.Id();

                await groupService.DeleteGroupAsync(id, userId!);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
                //return RedirectToAction(nameof(ErrorController.NotOwner), "Error");
            }

            return RedirectToAction(nameof(All));
        }
    }
}
