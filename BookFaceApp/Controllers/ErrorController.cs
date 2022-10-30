using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotOwner()
        {
            return View();
        }

        public IActionResult InvalidModel()
        {
            return View();
        }
    }
}
