
using COMP2139_Assignment1.Areas.NorthPole.Models;
using COMP2139_Assignment1.Data;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace COMP2139_Assignment1.Controllers
{
    [Area("NorthPole")]
    [Route("[area]/[controller]/[action]")]
    public class CarBookingsController : Controller
    {
        private readonly UserManager<NorthPoleUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CarBookingsController(ApplicationDbContext context,UserManager<NorthPoleUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int CarId)
        {
            ViewData["CarId"] = CarId;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int CarId)
        {
            var Car =await _context.Cars.FindAsync(CarId);
            if (Car == null)
            {
                return NotFound();
            }
            ViewData["carId"] = CarId;
            ViewData["PlateNumber"] = Car.PlateNumber;
            ViewData["City"] = Car.City;
            ViewData["PickupLocation"] = Car.PickUpLocation;
            ViewData["Brand"] = Car.Brand;
            ViewData["Model"] = Car.Model;
            ViewData["Price"] = Car.Price;
            ViewData["RentalCompany"] = Car.RentalCompany;
            ViewData["SearchString"] = TempData["SearchString"];

            return View();
        }
        public async Task<IActionResult> Create([Bind("BookedStartDate", "BookedEndDate", "CarId")] CarBooking booking)
        {
            var Car =await _context.Cars.FindAsync(booking.CarId);
            if (Car == null)
            {
                return NotFound();
            }
            booking.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["PlateNumber"] = Car.PlateNumber;
            ViewData["City"] = Car.City;
            ViewData["PickupLocation"] = Car.PickUpLocation;
            ViewData["Brand"] = Car.Brand;
            ViewData["Model"] = Car.Model;
            ViewData["Price"] = Car.Price;
            ViewData["RentalCompany"] = Car.RentalCompany;

            

            if (ModelState.IsValid)
            {
                if (booking.BookedEndDate < booking.BookedStartDate)
                {
                    ModelState.AddModelError("BookedEndDate", "End date must be equal or later than start date");
                    return View(booking);
                }
                if (booking.BookedStartDate < DateTime.Now.AddDays(-1))
                {
                    ModelState.AddModelError("BookedStartDate", "Start date cannot be earlier than today's date");
                    return View(booking);
                    

                }//Booking date intersect configure await 
                if (await BookingDatesIntersect(booking).ConfigureAwait(false))
                {
                    ModelState.AddModelError("", "Sorry, this date for this car is already booked");
                    return View(booking);
                }
                await _context.CarBookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Search", new { CarId = booking.CarId });
            }
            return View(booking);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {

            var booking = await _context.CarBookings
                        .Include(t => t.Car)
                        .FirstOrDefaultAsync(t => t.Id == Id);
            if (booking == null)
            {
                return NotFound();
            }
            var Car = await _context.Cars.FindAsync(booking.CarId);
            if (Car == null)
            {
                return NotFound();
            }
            ViewData["PlateNumber"] = Car.PlateNumber;
            ViewData["City"] = Car.City;
            ViewData["PickupLocation"] = Car.PickUpLocation;
            ViewData["Brand"] = Car.Brand;
            ViewData["Model"] = Car.Model;
            ViewData["Price"] = Car.Price;
            ViewData["RentalCompany"] = Car.RentalCompany;

            return View(booking);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, [Bind("Id","BookedStartDate", "BookedEndDate", "CarId")] CarBooking booking)
        {
            if (Id != booking.Id)
            {
                return NotFound();
            }
            var Car = await _context.Cars.FindAsync(booking.CarId);
            if (Car == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (booking.BookedEndDate < booking.BookedStartDate)
                {
                    ModelState.AddModelError("BookedEndDate", "End date must be equal or later than start date");
                    return View(booking);
                }
                if (booking.BookedStartDate < DateTime.Now.AddDays(-1))
                {
                    ModelState.AddModelError("BookedStartDate", "Start date cannot be earlier than today's date");
                    return View(booking);
                }
				if (await BookingDatesIntersect(booking).ConfigureAwait(false))
				{
					ModelState.AddModelError("", "Sorry, this date for this car is already booked");
					return View(booking);
				}
				_context.CarBookings.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Search", new { CarId = booking.CarId });
            }
            return View(booking);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var CarBooking = await _context.CarBookings.FirstOrDefaultAsync(cb => cb.Id == id);

            if (CarBooking == null)
            {
                return NotFound();
            }

            var Car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == CarBooking.CarId);
            if (Car == null)
            {
                return NotFound(); 
            }

            CarBooking.Car = Car;
            return View(CarBooking);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var CarBooking = await _context.CarBookings.FindAsync(id);


            if (CarBooking != null)
			{
                if (CarBooking.BookedStartDate < DateTime.Now.AddDays(1))
                {
                    ModelState.AddModelError("", "You cannot delete a booking within 24 hours of the start date.");
                    return View("Delete", CarBooking); 
                }

                _context.CarBookings.Remove(CarBooking);
				await _context.SaveChangesAsync();
                return RedirectToAction("Search", new { carId = CarBooking.CarId });
            }
			return NotFound();
		}
		public async Task<IActionResult> Search(int CarId)
        {
            var Car = await _context.Cars.FindAsync(CarId);

            if (Car != null)
            {
                var CarBookings = from cb in _context.CarBookings
                                  select cb;

                CarBookings = CarBookings
                    .Include(cb => cb.User)
			        .Where(c => c.CarId == CarId); 
                // Select User from Carbookings.UserId and store it in CarBookings.User
                ViewData["CarId"] = CarId;
                ViewData["PlateNumber"] = Car.PlateNumber;
                ViewData["City"] = Car.City;
                ViewData["PickupLocation"] = Car.PickUpLocation;
                ViewData["Brand"] = Car.Brand;
                ViewData["Model"] = Car.Model;
                ViewData["Price"] = Car.Price;
                ViewData["RentalCompany"] = Car.RentalCompany;

                var CarBookingsList = await CarBookings.ToListAsync();
                
                return View("Index", CarBookingsList);
            }

            else
            {
                return NotFound();
            }
        }
        // Helper function to only book if there is currently not a booking for said item
        private async Task<bool> BookingDatesIntersect(CarBooking newBooking)
        {
            var existingBookings = await _context.CarBookings
                .Where(b => b.CarId == newBooking.CarId && b.Id != newBooking.Id)
                .ToListAsync();

            foreach (var existingBooking in existingBookings)
            {
                if ((newBooking.BookedStartDate <= existingBooking.BookedEndDate && newBooking.BookedStartDate >= existingBooking.BookedStartDate) ||
                    (newBooking.BookedEndDate >= existingBooking.BookedStartDate) && (newBooking.BookedEndDate <= existingBooking.BookedEndDate))
                {
                    return true; 
                }
            }

            return false; 
        }

    }
}

