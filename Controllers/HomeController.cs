using Microsoft.AspNetCore.Mvc;

namespace freeschool_discipline_back.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
