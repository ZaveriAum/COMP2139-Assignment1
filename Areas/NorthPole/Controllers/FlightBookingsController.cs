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
        private readonly ILogger<FlightBookingsController> _logger;

        public FlightBookingsController(ApplicationDbContext context, UserManager<NorthPoleUser> userManager, ILogger<FlightBookingsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int FlightId)
        {
            _logger.LogInformation("Flight booking page for flight.");
            try {
                ViewData["FlightId"] = FlightId;
                return View();
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(int FlightId)
        {
            _logger.LogInformation("Create page for flight booking");
            try
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
                ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return View();
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FlightId", "PassengerName", "PassportNumber", "NumberOfPassenger", "UserId")] FlightBooking booking)
        {
            _logger.LogInformation("Create a car booking for user for the car entity.");
            try {
                var flight = await _context.Flights.FindAsync(booking.FlightId);
                if (ModelState.IsValid)
                {
                    int bookedSeats = _context.FlightBookings
                        .Where(b => b.FlightId == booking.FlightId)
                        .Sum(b => b.NumberOfPassenger);

                    if (booking.NumberOfPassenger + bookedSeats > flight.Seats)
                    {
                        ModelState.AddModelError("", "There is not enough seats in this flight");
                        return View(booking);
                    }
                    await _context.FlightBookings.AddAsync(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Search", new { FlightId = booking.FlightId });
                }
                return View(booking);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            _logger.LogInformation("Edit page the booking made");
            try {
                var booking = await _context.FlightBookings.Include(f => f.Flight).FirstOrDefaultAsync(f => f.Id == Id);
                if (booking == null)
                {
                    return NotFound();
                }
                var Flight = _context.Flights.Find(booking.FlightId);
                if (Flight == null)
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id", "FlightId", "PassengerName", "PassportNumber", "NumberOfPassenger")] FlightBooking booking)
        {
            _logger.LogInformation("Edit function for editing car booking made.");
            try {
                if (Id != booking.Id)
                {
                    return NotFound();
                }
                var Flight = await _context.Flights.FindAsync(booking.FlightId);
                if (Flight == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    _context.Flights.Update(Flight);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Search", new { FlightId = booking.FlightId });
                }
                return View(booking);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search(int FlightId)
        {
            _logger.LogInformation("Seach for booked flight.");
            try
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete Page for fligt booking.");
            try {
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("Function to cofirm delete for booked flight.");
            try
            {
                var FlightBooking = await _context.FlightBookings.FindAsync(id);


                if (FlightBooking != null)
                {
                    _context.FlightBookings.Remove(FlightBooking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Search", new { flightId = FlightBooking.FlightId });
                }
                return NotFound();
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }
    }
}
