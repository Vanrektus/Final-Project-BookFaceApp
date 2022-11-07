using BookFaceApp.Core.Constants;
using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Publication;
using BookFaceApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    [Authorize]
    public class PublicationController : Controller
    {
        private readonly IPublicationService publicationService;

        public PublicationController(
            IPublicationService _publicationService)
        {
            publicationService = _publicationService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await publicationService.GetAllPublicationsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new PublicationAddModel()
            {
                Categories = await publicationService.GetCategoriesAsync()
            };

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
                var userId = User.Id();

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

            TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";

            return RedirectToAction("InvalidPublication", "Error");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await publicationService.GetPublicationForEditAsync(id);

            if (model == null)
            {
                TempData[MessageConstant.ErrorMessage] = "The publication you are looking for was not found :(";

                return RedirectToAction("InvalidPublication", "Error");
            }

            var userId = User.Id();

            if (model.UserId != userId)
            {
                TempData[MessageConstant.ErrorMessage] = "You need to be the owner in order to perform this action!";

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

            await publicationService.EditPublicationAsync(model);

            return RedirectToAction(nameof(Details), new { model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> LikePublication(int id)
        {
            try
            {
                var userId = User.Id();

                await publicationService.LikePublicationAsync(id, userId!);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
		{
			try
            {
                var userId = User.Id();

                await publicationService.DeletePublicationAsync(id, userId!);
            }
			catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
                //return RedirectToAction("NotOwner", "Error");
            }

            return RedirectToAction(nameof(All));
        }
    }
}
