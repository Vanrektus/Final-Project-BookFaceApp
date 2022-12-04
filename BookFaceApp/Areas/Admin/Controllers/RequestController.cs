using BookFaceApp.Controllers;
using BookFaceApp.Core.Constants;
using BookFaceApp.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using static BookFaceApp.Controllers.Constants.ControllersConstants.ControllersNamesConstants;

namespace BookFaceApp.Areas.Admin.Controllers
{
    public class RequestController : AdminController
    {
        private readonly IGroupService groupService;

        public RequestController(IGroupService _groupService)
        {
            groupService = _groupService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await groupService.GetAllUnacceptedUsers();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int groupId, string userId)
        {
			if ((await groupService.ExistsByIdAsync(groupId)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The group you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidGroup), ErrorControllerName);
			}

			await groupService.AddUserToGroup(groupId, userId);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Deny(int groupId, string userId)
		{
			if ((await groupService.ExistsByIdAsync(groupId)) == false)
			{
				TempData[MessageConstant.ErrorMessage] = "The group you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidGroup), ErrorControllerName);
			}

			await groupService.RemoveUserFromGroup(groupId, userId);

            return RedirectToAction(nameof(All));
        }
    }
}
