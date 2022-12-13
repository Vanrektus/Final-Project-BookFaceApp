using BookFaceApp.Core.Constants;
using BookFaceApp.Core.Contracts;
using BookFaceApp.Extensions;
using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookFaceApp.Controllers.Constants.ControllersConstants.ControllersNamesConstants;

namespace BookFaceApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;
        private readonly IFileService fileService;

        public ProfileController(
            IProfileService _profileService,
            IFileService _fileService)
        {
            profileService = _profileService;
            fileService = _fileService;
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

        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    TempData[MessageConstant.ErrorMessage] = "You must choose a picture to upload!";

                    return RedirectToAction(nameof(MyProfile));
                }

                if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
                {
                    TempData[MessageConstant.ErrorMessage] = "Picture format must be JPEG or PNG!";

                    return RedirectToAction(nameof(ErrorController.InvalidPictureFormat), ErrorControllerName);
                }

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    var userId = base.User.Id();

                    var imageToString = file.FileName.EndsWith(".png")
                        ? "data:image/png;base64," + Convert
                        .ToBase64String(stream.ToArray(), 0, stream.ToArray().Length)
                        : "data:image/jpeg;base64," + Convert
                        .ToBase64String(stream.ToArray(), 0, stream.ToArray().Length);

                    var fileToSave = new ProfilePicture()
                    {
                        FileName = file.FileName,
                        Content = stream.ToArray(),
                        ImageToString = imageToString,
                        UserId = userId,
                    };

                    await fileService.SavePictureAsync(fileToSave);
                }

                return RedirectToAction(nameof(MyProfile));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
