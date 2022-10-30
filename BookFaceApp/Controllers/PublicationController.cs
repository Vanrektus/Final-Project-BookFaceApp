using BookFaceApp.Contracts;
using BookFaceApp.Data.Common;
using BookFaceApp.Models.Publication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookFaceApp.Controllers
{
    [Authorize]
    public class PublicationController : Controller
    {
        private readonly IPublicationService publicationService;
        private readonly IRepository repo;

        public PublicationController(
            IPublicationService _publicationService,
            IRepository _repo)
        {
            publicationService = _publicationService;
            repo = _repo;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await publicationService.GetAllPublicationsAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new PublicationAddModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PublicationAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                await publicationService.AddPublicationAsync(model, userId!);

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
            var model = await publicationService.GetOnePublicationAsync(id);

            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("InvalidPublication", "Error");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await publicationService.GetPublicationForEditAsync(id);

            if (model == null)
            {
                return RedirectToAction("InvalidPublication", "Error");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (model.UserId != userId)
            {
                return RedirectToAction("NotOwner", "Error");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PublicationEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await publicationService.EditPublication(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult AddComment()
        {
            var model = new PublicationAddCommentModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(PublicationAddCommentModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                await publicationService.AddCommentAsync(model, id, userId!);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> LikePublication(int id)
        {
            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                await publicationService.LikePublication(id, userId!);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(All));
        }
    }
}
