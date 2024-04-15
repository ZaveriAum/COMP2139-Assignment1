using COMP2139_Assignment1.Areas.NorthPole.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace COMP2139_Assignment1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Calling Home Page.");
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(null);
            }
        }
        public IActionResult About()
        {
            _logger.LogInformation("Calling About Page.");
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(null);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Calling Error Page");
            try
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(new ErrorViewModel { });
            }
        }

        public IActionResult NotFound(int statusCode)
        {
            _logger.LogInformation("Calling Not Found Page.");
            try
            {
                if (statusCode == 404)
                {
                    return View("NotFound");
                }
                return View("Error");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(new ErrorViewModel { });
            }
        }
    }
}
