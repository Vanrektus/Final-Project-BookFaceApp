using BookFaceApp.Core.Contracts;
using BookFaceApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPublicationService publicationService;

        public HomeController(
            IPublicationService _publicationService)
        {
            publicationService = _publicationService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await publicationService.GetRandomPublications();

            return View(model);
        }
    }
}