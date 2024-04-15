using COMP2139_Assignment1.Areas.NorthPole.Models;
using COMP2139_Assignment1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace COMP2139_Assignment1.Areas.NorthPole.Controllers
{
    [Area("NorthPole")]
    [Route("[area]/[controller]/[action]")]

    public class CarReviewsController : Controller
    {
        private readonly UserManager<NorthPoleUser> _userManager;
        private readonly ApplicationDbContext _context;
        public CarReviewsController(ApplicationDbContext context, UserManager<NorthPoleUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("Index/{CarId:int}")]
        public async Task<IActionResult> Index(int CarId)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == CarId);
            var carReviews = await _context.CarReviews
				.Where(cr => cr.CarId == CarId)
                .Include(cr => cr.User)
                .ToListAsync();
            ViewBag.CarId = CarId;
            ViewBag.carModel = car?.Model;
            ViewBag.carBrand = car?.Brand;
            return View(carReviews);
        }

        [HttpGet("Delete/{reviewId: int}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var carReview = await _context.CarReviews.Include(cr => cr.User).FirstOrDefaultAsync(p => p.Id== reviewId);
            if (carReview == null)
            {
                return NotFound();
            }
            return View(carReview);
        }

        [HttpPost("DeleteConfirmed/{Id:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var carReview = _context.CarReviews.Find(Id);
            if (carReview != null)
            {
                _context.CarReviews.Remove(carReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { carId = carReview.CarId});
            }
            return NotFound();
        }

        [HttpGet("Create/{carId:int}")]
        [Authorize]
        public async Task<IActionResult> Create(int carId)
        {
            ViewBag.carId = carId;
            ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        [HttpPost("Create/{carId:int}")]
        [Authorize]
        public async Task<IActionResult> Create(int carId, CarReview review)
        {
            review.CarId= carId;
            if(review.Comment == null)
            {
                review.Comment = "No Comment Added";
            }
            review.DatePosted = DateTime.Now;
            if(ModelState.IsValid)
            {
                _context.Update(review);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), new { carId = review.CarId });
        }
    }
}
