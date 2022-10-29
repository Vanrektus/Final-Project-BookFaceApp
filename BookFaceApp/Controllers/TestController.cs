using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Test()
        {
            return View();
        }
    }
}
