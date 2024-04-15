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
    public class FlightReviewsController : Controller
    {
        private readonly UserManager<NorthPoleUser> _userManager;
        private readonly ApplicationDbContext _context;
        public FlightReviewsController(ApplicationDbContext context, UserManager<NorthPoleUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("Index/{FlightId:int}")]
        public async Task<IActionResult> Index(int FlightId)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == FlightId);
            var flightReviews = await _context.FlightReviews
                .Where(fr => fr.FlightId == FlightId)
                .Include(fr => fr.User)
                .ToListAsync();
            ViewBag.FlightId = FlightId;
            ViewBag.Airline = flight?.Airline;
            ViewBag.Number = flight?.FlightNumber;
            return View(flightReviews);
        }

        [HttpGet("Delete/{reviewId:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var flightReview = await _context.FlightReviews.Include(fr => fr.User).FirstOrDefaultAsync(p => p.Id == reviewId);
            if (flightReview == null)
            {
                return NotFound();
            }
            return View(flightReview);
        }

        [HttpPost("DeleteConfirmed/{Id:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var flightReview = _context.FlightReviews.Find(Id);
            if (flightReview != null)
            {
                _context.FlightReviews.Remove(flightReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { flightId = flightReview.FlightId});
            }
            return NotFound();
        }

        [HttpGet("Create/{flightId:int}")]
        [Authorize]
        public async Task<IActionResult> Create(int flightId)
        {
            ViewBag.flightId = flightId;
            ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        [HttpPost("Create/{flightId:int}")]
        [Authorize]
        public async Task<IActionResult> Create(int flightId, FlightReview review)
        {
            review.FlightId = flightId;
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
            return RedirectToAction(nameof(Index), new { flightId = review.FlightId});
        }
    }
}
