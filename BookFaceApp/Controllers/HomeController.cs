using BookFaceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookFaceApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Publication");
            }

            return View();
        }
    }
}