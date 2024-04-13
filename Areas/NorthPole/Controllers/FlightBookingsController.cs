using COMP2139_Assignment1.Areas.NorthPole.Models;
using COMP2139_Assignment1.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.Security.Claims;

namespace COMP2139_Assignment1.Controllers
{
    [Area("NorthPole")]
    [Route("[area]/[controller]/[action]")]
    public class FlightBookingsController : Controller
    {
        private readonly UserManager<NorthPoleUser> _userManager;
        private readonly ApplicationDbContext _context;

        public FlightBookingsController(ApplicationDbContext context, UserManager<NorthPoleUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int FlightId)
        {
            ViewData["FlightId"] = FlightId;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int FlightId)
        {
            var Flight = await _context.Flights.FindAsync(FlightId);
            if (Flight == null)
            {
                return NotFound();
            }

            int bookedSeats = await _context.FlightBookings
                .Where(b => b.FlightId == FlightId)
                .SumAsync(b => b.NumberOfPassenger);
            ViewBag.ShowAlert = false;

            if (bookedSeats >= Flight.Seats)
            {
                ViewBag.ShowAlert = true;
                //return NotFound("There is no more seat for this flight");

            }

            ViewData["FlightId"] = FlightId;
            ViewData["FlightNumber"] = Flight.FlightNumber;
            ViewData["Airline"] = Flight.Airline;
            ViewData["DepartureDate"] = Flight.DepartureDate;
            ViewData["DepartureTime"] = Flight.DepartureTime;
            ViewData["ArrivalDate"] = Flight.ArrivalDate;
            ViewData["Price"] = Flight.Price;
            ViewData["From"] = Flight.From;
            ViewData["To"] = Flight.To;
            ViewData["Seats"] = Flight.Seats;
            return View();
        }

        public async Task<IActionResult> Create([Bind("FlightId", "PassengerName", "PassportNumber", "NumberOfPassenger")] FlightBooking booking)
        {
            var flight = _context.Flights.Find(booking.FlightId);
            booking.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                int bookedSeats = _context.FlightBookings
                    .Where(b => b.FlightId == booking.FlightId)
                    .Sum(b => b.NumberOfPassenger);

                if (booking.NumberOfPassenger+bookedSeats > flight.Seats)
                {
                    ModelState.AddModelError("", "There is not enough seats in this flight");
                    return View(booking);
                }
                await _context.FlightBookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Search", new { FlightId = booking.FlightId });
            }
            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var booking = await _context.FlightBookings.Include(f => f.Flight).FirstOrDefaultAsync(f => f.Id == Id);
            if (booking == null)
            {
                return NotFound();
            }
            var Flight = _context.Flights.Find(booking.FlightId);
            if(Flight == null)
            {
                return NotFound();
            }
            ViewData["FlightNumber"] = Flight.FlightNumber;
            ViewData["Airline"] = Flight.Airline;
            ViewData["From"] = Flight.From;
            ViewData["To"] = Flight.To;
            ViewData["DepartureDate"] = Flight.DepartureDate;
            ViewData["ArrivalDate"] = Flight.ArrivalDate;
            ViewData["DepartureTime"] = Flight.DepartureTime;
            ViewData["ArrivalTime"] = Flight.ArrivalTime;
            ViewData["Price"] = Flight.Price;
            ViewData["Seats"] = Flight.Seats;
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id", "FlightId", "PassengerName", "PassportNumber", "NumberOfPassenger")] FlightBooking booking)
        {
            if (Id != booking.Id)
            {
                return NotFound();
            }
            var Flight = await _context.Flights.FindAsync(booking.FlightId);
            if (Flight == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Flights.Update(Flight);
                await _context.SaveChangesAsync();
                return RedirectToAction("Search", new {FlightId = booking.FlightId});
            }
            return View(booking);
        }
        public async Task<IActionResult> Search(int FlightId)
        {
            var Flight = await _context.Flights.FindAsync(FlightId);

            if (Flight != null)
            {
                var FlightBooking = from fb in _context.FlightBookings
                                  select fb;

                FlightBooking = FlightBooking.Include(fb => fb.User).Where(f => f.FlightId == FlightId);
                ViewData["FlightId"] = FlightId;
                ViewData["FlightNumber"] = Flight.FlightNumber;
                ViewData["Airline"] = Flight.Airline;
                ViewData["DepartureDate"] = Flight.DepartureDate;
                ViewData["DepartureTime"] = Flight.DepartureTime;
                ViewData["ArrivalDate"] = Flight.ArrivalDate;
                ViewData["Price"] = Flight.Price;
                ViewData["From"] = Flight.From;
                ViewData["To"] = Flight.To;
                ViewData["Seats"] = Flight.Seats;

                var FlightBookingList = await FlightBooking.ToListAsync();

                return View("Index", FlightBookingList);
            }

            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var FlightBooking = await _context.FlightBookings.FirstOrDefaultAsync(cb => cb.Id == id);

            if (FlightBooking == null)
            {
                return NotFound();
            }

            var Flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == FlightBooking.FlightId);
            if (Flight == null)
            {
                return NotFound();
            }

            FlightBooking.Flight = Flight;
            return View(FlightBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var FlightBooking = await _context.FlightBookings.FindAsync(id);


            if (FlightBooking != null)
            {
                _context.FlightBookings.Remove(FlightBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Search", new { flightId = FlightBooking.FlightId });
            }
            return NotFound();
        }
    }
}
