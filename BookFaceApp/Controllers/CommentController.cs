using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
