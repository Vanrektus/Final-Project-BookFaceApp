using BookFaceApp.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
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
            var model = await publicationService.GetTop3PublicationsAsync();

            return View(model);
        }
    }
}