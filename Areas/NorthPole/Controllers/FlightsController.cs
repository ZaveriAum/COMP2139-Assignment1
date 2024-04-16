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
		private readonly ILogger<FlightsController> _logger;

		public FlightsController(ApplicationDbContext context, ILogger<FlightsController> logger)
		{
			_logger = logger;
			_context = context;
		}


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			_logger.LogInformation("Search page for available flight");
			try
			{
				var flights = await _context.Flights.ToListAsync();
				return View(flights);
			}catch(Exception ex)
			{
				_logger.LogError(ex.Message);
                return View();
            }
        }

/*		[HttpGet("SearchFlight")]
		public async Task<IActionResult> SearchFlight()
		{
			_logger.LogInformation("Search flights page.");
			try {
				var flights = await _context.Flights.ToListAsync();

				return View(flights);
			}catch(Exception ex)
			{
				_logger.LogError(ex.Message);
                return View();
            }
        }*/

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
		public IActionResult Create() 
		{
			_logger.LogInformation("Create page for Flight entity.");
			try
			{
				return View();
			}catch(Exception ex)
			{
				_logger.LogError(ex.Message);
                return View();
            }
        }

		[Authorize(Roles = "SuperAdmin,Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Flight flight)
		{
			_logger.LogInformation("Create page for flight entity.");
			try {
				Type type = flight.Price.GetType();
				if (ModelState.IsValid)
				{
					await _context.Flights.AddAsync(flight);
					await _context.SaveChangesAsync();
					return RedirectToAction("Index");
				}
				return View(flight);
			}catch(Exception ex)
			{
				_logger.LogError(ex.Message);
                return View();
            }
        }

		[HttpGet("Create/{flightId:int}")]
		public async Task<IActionResult> Details(int flightId)
		{
			_logger.LogInformation("Details page for flight entity.");
			try {
				var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == flightId);
				if (flight == null)
				{
					return NotFound();
				}
				return View(flight);
			}catch(Exception ex)
			{
				_logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

		[Authorize(Roles = "SuperAdmin,Admin")]
		[HttpGet]
		public IActionResult Edit(int flightId)
		{
			_logger.LogInformation("Edit page for flight entity.");
			try {
				var flight = _context.Flights.Find(flightId);
				if (flight == null)
					return NotFound();
				return View(flight);
			}catch(Exception ex)
			{
				_logger.LogError(ex.Message);
                return View();
            }
        }

		[Authorize(Roles = "SuperAdmin,Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int flightId, [Bind("FlightId", "FlightNumber", "Airline", "DepartureDate", "DepartureTime", "ArrivalDate", "ArrivalTime", "Price", "From", "To", "Seats")] Flight flight)
		{
			_logger.LogInformation("Edit funciton for the flight entity.");
			try {
				if (flightId != flight.FlightId)
				{
					return NotFound();
				}
				if (ModelState.IsValid)
				{
					try
					{
						_context.Update(flight);
						await _context.SaveChangesAsync();
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!await FlightExists(flight.FlightId))
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
			}catch(Exception ex)
			{
				_logger.LogError(ex.Message);
                return View();
            }
        }

		public async Task<bool> FlightExists(int flightId)
		{
			_logger.LogInformation("Check if the flight exists.");
			try {
				return await _context.Flights.AnyAsync(e => e.FlightId == flightId);
			}catch(Exception ex)
			{
				_logger.LogError(ex.Message);
				return false;
			}
		}

		[Authorize(Roles = "SuperAdmin,Admin")]
		[HttpGet]
		public async Task<IActionResult> Delete(int flightId)
		{
			_logger.LogInformation("Delete page for flight entity.");
			try {
				var flight = await _context.Flights.FindAsync(flightId);
				if (flight == null)
				{
					return NotFound();
				}
				return View(flight);
			}catch(Exception ex)
			{
				_logger.LogError(ex.Message);
                return View();
            }
        }

		[Authorize(Roles = "SuperAdmin,Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int flightId)
		{
			_logger.LogInformation("Delete function for flight entity.");
			try {
				var flight = _context.Flights.Find(flightId);
				if (flight != null)
				{
					_context.Flights.Remove(flight);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				return NotFound();
			}catch(Exception ex)
			{
				_logger.LogError(ex.Message);
                return View();
            }
        }

		[HttpGet]
        public async Task<IActionResult> SearchFlight(string searchStringFrom, string searchStringTo, DateOnly searchStringDepartureDate, int searchPassengerNum)
		{
			_logger.LogInformation($"Search for information: searchStringTo: {searchStringTo}, searchStringFrom: {searchStringFrom}");
			try {
                if (_context.Flights == null)
                {
                    return Problem("Sorry, there are currently no flights available at the moment!");
                }
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
					flightsQuery =  flightsQuery.Where(f => f.Seats - _context.FlightBookings
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
            catch (Exception ex)
			{
				_logger.LogError(ex.Message);
                return View();
            }
        }
	}
}
