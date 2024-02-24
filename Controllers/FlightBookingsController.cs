using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;

namespace COMP2139_Assignment1.Controllers
{
    public class FlightBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int FlightId)
        {
            ViewData["FlightId"] = FlightId;
            return View();
        }

        [HttpGet]
        public IActionResult Create(int FlightId)
        {
            var Flight = _context.Flights.Find(FlightId);
            if (Flight == null)
            {
                return NotFound();
            }

            int bookedSeats = _context.FlightBookings.Count(b => b.FlightId == FlightId);
            if (bookedSeats >= Flight.Seats)
            {
                return NotFound("There is no more seats on this flight");
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

        public IActionResult Create([Bind("FlightId", "PassengerName", "PassportNumber")] FlightBooking booking)
        {
            Console.WriteLine($"FlightId: {booking.FlightId}");
            Console.WriteLine($"PassengerName: {booking.PassengerName}");
            Console.WriteLine($"PassportNumber: {booking.PassportNumber}");

            if (ModelState.IsValid)
            {
                _context.FlightBookings.Add(booking);
                _context.SaveChanges();
                return RedirectToAction("Search", new { FlightId = booking.FlightId });
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

                FlightBooking = FlightBooking.Where(f => f.FlightId == FlightId);
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

        public IActionResult Delete(int id)
        {
            var FlightBooking = _context.FlightBookings.FirstOrDefault(cb => cb.Id == id);

            if (FlightBooking == null)
            {
                return NotFound();
            }

            var Flight = _context.Flights.FirstOrDefault(f => f.FlightId == FlightBooking.FlightId);
            if (Flight == null)
            {
                return NotFound();
            }

            FlightBooking.Flight = Flight;
            return View(FlightBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var FlightBooking = _context.FlightBookings.Find(id);


            if (FlightBooking != null)
            {
                if (FlightBooking.BookingTime <DateTime.Now.AddDays(1))
                {
                    ModelState.AddModelError("", "You cannot delete a booking within 24 hours of the start date.");
                    return View("Delete", FlightBooking);
                }


                _context.FlightBookings.Remove(FlightBooking);
                _context.SaveChanges();
                return RedirectToAction("Search", new { flightId = FlightBooking.FlightId });
            }
            return NotFound();
        }
    }
}
