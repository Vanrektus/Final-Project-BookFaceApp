using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    public class TestController : Controller
    {
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Test()
        {
            return View();
        }
    }
}
