using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COMP2139_Assignment1.Areas.NorthPole.Controllers
{
	[Authorize("Admin,SuperAdmin")]
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
