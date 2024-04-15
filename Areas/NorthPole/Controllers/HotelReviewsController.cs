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
    public class HotelReviewsController : Controller
    {
        private readonly UserManager<NorthPoleUser> _userManager;
        private readonly ApplicationDbContext _context;
        public HotelReviewsController(ApplicationDbContext context, UserManager<NorthPoleUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("Index/{HotelId:int}")]
        public async Task<IActionResult> Index(int HotelId)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId== HotelId);
            var hotelReviews = await _context.HotelReviews
                .Where(hr => hr.HotelId == HotelId)
                .Include(cr => cr.User)
                .ToListAsync();
            ViewBag.HotelId = HotelId;
            ViewBag.HotelName = hotel.HotelName;
            return View(hotelReviews);
        }

        [HttpGet("Delete/{reviewId:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var hotelReview = await _context.HotelReviews.Include(hr => hr.User).FirstOrDefaultAsync(p => p.Id == reviewId);
            if (hotelReview == null)
            {
                return NotFound();
            }
            return View(hotelReview);
        }

        [HttpPost("DeleteConfirmed/{Id:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var hotelReview = _context.HotelReviews.Find(Id);
            if (hotelReview != null)
            {
                _context.HotelReviews.Remove(hotelReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { hotelId = hotelReview.HotelId});
            }
            return NotFound();
        }

        [HttpGet("Create/{hotelId:int}")]
        [Authorize]
        public async Task<IActionResult> Create(int hotelId)
        {
            ViewBag.hotelId = hotelId;
            ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        [HttpPost("Create/{hotelId:int}")]
        [Authorize]
        public async Task<IActionResult> Create(int hotelId, HotelReview review)
        {
            Console.WriteLine(review.Rating);
            review.HotelId = hotelId;
            if (review.Comment == null)
            {
                review.Comment = "No Comment Added";
            }
            review.DatePosted = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Update(review);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), new { hotelId = review.HotelId});
        }
    }
}
