using BookFaceApp.Core.Contracts;
using BookFaceApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService _profileService)
        {
            profileService = _profileService;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var userId = base.User.Id();

            var model = await profileService.GetMyProfilePublicationsAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> User(string id)
        {
            var model = await profileService.GetUserProfilePublicationsAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await profileService.GetAllUsersAsync();

            return View(model);
        }
    }
}
