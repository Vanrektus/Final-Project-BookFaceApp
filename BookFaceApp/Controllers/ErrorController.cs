using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    [Authorize]
    public class ErrorController : Controller
    {
        public IActionResult NotOwner()
        {
            return View();
        }

        public IActionResult InvalidPublication()
        {
            return View();
        }

        public IActionResult InvalidComment()
        {
            return View();
        }

        public IActionResult InvalidCategory()
        {
            return View();
        }

        public IActionResult InvalidGroup()
        {
            return View();
        }

        public IActionResult CreationError()
        {
            return View();
        }

        public IActionResult InvalidRole()
        {
            return View();
        }
        public IActionResult NotInGroup()
        {
            return View();
        }

        public IActionResult SomethingWentWrong()
        {
            return View();
        }

		public IActionResult InvalidUser()
		{
			return View();
		}
	}
}
