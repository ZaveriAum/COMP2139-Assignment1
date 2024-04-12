using COMP2139_Assignment1.Areas.NorthPole.Models;
using COMP2139_Assignment1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Controllers
{
    [Area("NorthPole")]
    [Route("[area]/[controller]/[action]")]
    public class FlightsController : Controller
	{

        private readonly ApplicationDbContext _context;

		public FlightsController(ApplicationDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		public IActionResult Index()
		{

            return View(_context.Flights.ToList());
		}

		[HttpGet]
		public IActionResult SearchFlight()
		{
			return View(_context.Flights.ToList());
		}
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet("Create")]
		public IActionResult Create() 
		{
			return View();
		}
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Flight flight)
		{
            Type type = flight.Price.GetType();
			if (ModelState.IsValid)
			{
				_context.Flights.Add(flight);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(flight);
		}

		[HttpGet]
		public IActionResult Details(int flightId)
		{
			var flight = _context.Flights.FirstOrDefault(f=>f.FlightId == flightId);
			if (flight == null)
			{
				return NotFound();
			}
			return View(flight);
		}
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
		public IActionResult Edit(int flightId)
		{
			var flight = _context.Flights.Find(flightId);
			if(flight == null)
				return NotFound();
			return View(flight);
		}
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int flightId, [Bind("FlightId", "FlightNumber", "Airline", "DepartureDate", "DepartureTime", "ArrivalDate", "ArrivalTime", "Price", "From", "To", "Seats")] Flight flight)
		{
			if(flightId != flight.FlightId)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(flight);
					_context.SaveChanges();
				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
			return View(flight);
		}
        private bool FlightExists(int flightId)
        {
            return _context.Flights.Any(e => e.FlightId == flightId);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int flightId)
		{
			var flight = _context.Flights.Find(flightId);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
            }
			return NotFound();
        }

		[HttpGet]
		public async Task<IActionResult> Search(string searchStringFrom, string searchStringTo, DateOnly searchStringDepartureDate, int searchPassengerNum)
		{
			var flightsQuery = from f in _context.Flights select f;

			bool searchPerformed = !String.IsNullOrEmpty(searchStringFrom) || !String.IsNullOrEmpty(searchStringTo);
            if (searchPerformed)
            {
                flightsQuery = flightsQuery.Where(f => f.From.Contains(searchStringFrom) &&
                                                          f.To.Contains(searchStringTo) &&
                                                          f.DepartureDate == searchStringDepartureDate);
            }

			if (searchPassengerNum > 0)
			{
				flightsQuery = flightsQuery.Where(f => f.Seats - _context.FlightBookings
															 .Where(b => b.FlightId == f.FlightId)
															 .Sum(b => b.NumberOfPassenger) >= searchPassengerNum);
			}


			var flights = await flightsQuery.ToListAsync();
			ViewData["SearchPerformed"] = searchPerformed;
			ViewData["SearchStringFrom"] = searchStringFrom;
			ViewData["SearchStringTo"] = searchStringTo;
			ViewData["searchStringDepartureDate"] = searchStringDepartureDate;
            ViewData["searchPassengerNum"] = searchPassengerNum;

            return View("SearchFlight", flights);
		}
	}
}
