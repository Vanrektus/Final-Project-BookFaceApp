using BookFaceApp.Core.Contracts;
using BookFaceApp.Extensions;
using Microsoft.AspNetCore.Mvc;

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
            await groupService.AddUserToGroup(groupId, userId);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Deny(int groupId, string userId)
        {
            await groupService.RemoveUserFromGroup(groupId, userId);

            return RedirectToAction(nameof(All));
        }
    }
}
