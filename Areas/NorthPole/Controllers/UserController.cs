using COMP2139_Assignment1.Areas.NorthPole.Models;
using COMP2139_Assignment1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Areas.NorthPole.Controllers
{
	[Area("NorthPole")]
	[Route("[area]/[controller]/[action]")]
	[Authorize(Roles ="Admin,SuperAdmin")]
	public class UserController : Controller
	{
		private readonly ApplicationDbContext _context;

		public UserController(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index(string userId)
		{
			NorthPoleUser user = await _context.Users.FindAsync(userId);

			if (user != null)
			{
				return View(user);
			}
			else
			{
				return NotFound();
			}
		}
        public async Task<IActionResult> Delete(string userId)
        {
            var User = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
             var User = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);


            if (User != null)
            {
                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
