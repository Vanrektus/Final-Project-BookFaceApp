using BookFaceApp.Controllers;
using BookFaceApp.Core.Constants;
using BookFaceApp.Core.Models.Role;
using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Controllers.Constants.ControllersConstants.ControllersNamesConstants;

namespace BookFaceApp.Areas.Admin.Controllers
{
	public class AdminController : BaseController
	{
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly UserManager<User> userManager;

		public AdminController(
			RoleManager<IdentityRole> _roleManager,
			UserManager<User> _userManager)
		{
			roleManager = _roleManager;
			userManager = _userManager;
		}

		public IActionResult Index()
        {
            return View();
        }

		public IActionResult Roles()
		{
			return View(roleManager.Roles);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([Required] string name)
		{
			if (!ModelState.IsValid)
			{
				return View(name);
			}

			IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));

			if (!result.Succeeded)
			{
				TempData[MessageConstant.ErrorMessage] = "Could not create role :(";

				return RedirectToAction(nameof(ErrorController.CreationError), ErrorControllerName);
			}

			return RedirectToAction(nameof(Roles));
		}

		[HttpGet]
		public async Task<IActionResult> Update(string id)
		{
			IdentityRole role = await roleManager.FindByIdAsync(id);
			List<User> users = new List<User>();
			List<User> nonUsers = new List<User>();

			var allUsers = await userManager.Users.ToListAsync();

			foreach (User user in allUsers)
			{
				var list = await userManager.IsInRoleAsync(user, role.Name) ? users : nonUsers;

				list.Add(user);
			}

			return View(new RoleViewModel
			{
				Role = role,
				Users = users,
				NonUsers = nonUsers
			});
		}

		[HttpPost]
		public async Task<IActionResult> Update(RoleEditModel model)
		{
			IdentityResult result;

			if (ModelState.IsValid)
			{
				foreach (string userId in model.AddIds ?? new string[] { })
				{
					User user = await userManager.FindByIdAsync(userId);

					if (user != null)
					{
						result = await userManager.AddToRoleAsync(user, model.RoleName);

						if (!result.Succeeded)
						{
							TempData[MessageConstant.ErrorMessage] = "Could not get users to add :(";

							return RedirectToAction(nameof(ErrorController.CreationError), ErrorControllerName);
						}
					}
				}

				foreach (string userId in model.DeleteIds ?? new string[] { })
				{
					User user = await userManager.FindByIdAsync(userId);

					if (user != null)
					{
						result = await userManager.RemoveFromRoleAsync(user, model.RoleName);

						if (!result.Succeeded)
						{
							TempData[MessageConstant.ErrorMessage] = "Could not get users to delete :(";

							return RedirectToAction(nameof(ErrorController.CreationError), ErrorControllerName);
						}
					}
				}
			}

			if (!ModelState.IsValid)
			{
				return await Update(model.RoleId);
			}

			return RedirectToAction(nameof(Roles));
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			var role = await roleManager.FindByIdAsync(id);

			if (role == null)
			{
				TempData[MessageConstant.ErrorMessage] = "The role you are looking for was not found :(";

				return RedirectToAction(nameof(ErrorController.InvalidRole), ErrorControllerName);
			}

			await roleManager.DeleteAsync(role);

			return RedirectToAction(nameof(Roles));
		}
	}
}