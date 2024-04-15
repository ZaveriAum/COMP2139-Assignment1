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
        private readonly ILogger<UserController> _logger;

		public UserController(ApplicationDbContext context, ILogger<UserController> logger)
		{
			_context = context;
            _logger = logger;
		}

		public async Task<IActionResult> Index(string userId)
		{
            _logger.LogInformation($"Index page called for user with user id: {userId}.");
            try
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        public async Task<IActionResult> Delete(string userId)
        {
            _logger.LogInformation($"Delete action invoked with userId: {userId}");
            try {
                var User = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (User == null)
                {
                    return NotFound();
                }

                return View(User);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _logger.LogInformation($"Deleting user with: user id {id}.");
            try
            {
                var User = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);


                if (User != null)
                {
                    _context.Users.Remove(User);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return NotFound();
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
