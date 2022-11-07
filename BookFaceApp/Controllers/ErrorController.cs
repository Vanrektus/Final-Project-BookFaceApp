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
    }
}
