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
            var userId = User.Id();

            var model = await profileService.GetMyProfilePublicationsAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(string username)
        {
            var model = await profileService.GetUserProfilePublicationsAsync(username);

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
