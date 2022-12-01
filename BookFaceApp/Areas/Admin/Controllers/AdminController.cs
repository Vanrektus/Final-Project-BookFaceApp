using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}