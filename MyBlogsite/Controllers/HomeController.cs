using Microsoft.AspNetCore.Mvc;

namespace MyBlogsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
