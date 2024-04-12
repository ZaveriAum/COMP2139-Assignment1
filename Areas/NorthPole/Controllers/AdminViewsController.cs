using Microsoft.AspNetCore.Mvc;

namespace COMP2139_Assignment1.Areas.NorthPole.Controllers
{
    public class AdminViewsController : Controller
    {
        [Area("NorthPole")]
        [Route("[area]/[controller]/[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
