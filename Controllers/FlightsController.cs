using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Controllers
{
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

		[HttpGet("AddFlight")]
		public IActionResult AddFlight() 
		{
			return View();
		}

		[HttpPost("AddFlight")]
		[ValidateAntiForgeryToken]
		public IActionResult AddFlight(Flight flight)
		{
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

		[HttpGet]
		public IActionResult Edit(int flightId)
		{
			var flight = _context.Flights.Find(flightId);
			if(flight == null)
				return NotFound();
			return View(flight);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int flightId, [Bind("FlightNumber", "Airline", "ArrivalTime", "DepartureTime", "Price", "From", "To", "Seats")] Flight flight)
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
    }
}
