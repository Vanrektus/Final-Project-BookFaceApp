using BookFaceApp.Core.Contracts;
using BookFaceApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IPublicationService publicationService;

        public ProfileController(IPublicationService _publicationService)
        {
            publicationService = _publicationService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Id();

            var model = await publicationService.GetUserPublicationsAsync(userId!);

            return View(model);
        }

        //public async Task<IActionResult> MyProfile()
        //{

        //}

        public async Task<IActionResult> UserProfile(string UserName)
        {
            var model = await publicationService.GetUserPublicationsTestAsync(UserName);

            return View(model);
        }
    }
}
