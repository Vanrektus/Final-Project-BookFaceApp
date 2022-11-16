using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
