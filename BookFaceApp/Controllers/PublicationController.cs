﻿using BookFaceApp.Contracts;
using BookFaceApp.Data.Common;
using BookFaceApp.Data.Entities;
using BookFaceApp.Models.Publication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var model = await publicationService.GetAllAsync();

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
            var entities = await publicationService.GetAllAsync();

            var model = entities
                .Where(p => p.Id == id)
                .Select(p => new PublicationViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl,
                    UserId = p.UserId,
                    PublicationsComments = p.PublicationsComments,
                })
                .FirstOrDefault();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entities = await publicationService.GetAllAsync();

            var model = entities
                .Where(p => p.Id == id)
                .Select(p => new PublicationEditModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl,
                    UserId = p.UserId
                })
                .FirstOrDefault();

            if (model != null && (model.UserId == User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value))
            {
                return View(model);
            }

            return RedirectToAction("NotOwner", "Error");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PublicationEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var publication = await repo.GetByIdAsync<Publication>(model.Id);

            if (publication != null)
            {
                publication.Title = model.Title;
                publication.ImageUrl = model.ImageUrl;
            }

            await repo.SaveChangesAsync();

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

        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}