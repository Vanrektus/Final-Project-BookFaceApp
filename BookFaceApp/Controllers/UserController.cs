using BookFaceApp.Core.Models.User;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepository repo;

        public UserController(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            RoleManager<IdentityRole> _roleManager,
            IRepository _repo)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            repo = _repo;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(PublicationController.All), "Publication");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(UserController.Login), "User");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(PublicationController.All), "Publication");
            }

            var model = new LoginViewModel();

            //model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login!");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "User", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (remoteError != null)
            {
                TempData["ErrorMessage"] = $"Error from external provider: {remoteError}";

                return RedirectToAction("Login", new { ReturnUrl = returnUrl });
            }

            var info = await signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                TempData["ErrorMessage"] = "Error loading external login information";

                return RedirectToAction("Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Register");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ManageRoles()
        {
            //await roleManager.CreateAsync(new IdentityRole()
            //{
            //    Name = "Administrator"
            //});

            //var getRole = await roleManager.FindByNameAsync("roleName");

            //if (getRole != null)
            //{
            //    await roleManager.DeleteAsync(getRole);
            //}

            var user = await repo.GetByIdAsync<User>("dea12856-c198-4129-b3f3-b893d8395082");

            await userManager.AddToRoleAsync(user, "Administrator");

            return Ok();
        }
    }
}
