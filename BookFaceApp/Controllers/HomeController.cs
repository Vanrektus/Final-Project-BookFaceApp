using BookFaceApp.Core.Contracts;
using BookFaceApp.Extensions;
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
            //if (User.Id() != null)
            //{
            //    return RedirectToAction(nameof(PublicationController.All), "Publication");
            //}

            var model = await publicationService.GetTop3PublicationsAsync();

            return View(model);
        }
    }
}