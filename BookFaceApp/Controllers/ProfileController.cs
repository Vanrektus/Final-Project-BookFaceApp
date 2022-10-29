using BookFaceApp.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var userId = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var model = await publicationService.GetMineAsync(userId!);

            return View(model);
        }
    }
}
