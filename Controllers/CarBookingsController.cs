
using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Controllers
{
    public class CarBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int CarId)
        {
            ViewData["CarId"] = CarId;
            return View();
        }

        [HttpGet]
        public IActionResult Create(int CarId)
        {
            var Car = _context.Cars.Find(CarId);
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
        public IActionResult Create([Bind("BookedStartDate", "BookedEndDate", "CarId")] CarBooking booking)
        {
            Console.WriteLine($"BookedStartDate: {booking.BookedStartDate}");
            Console.WriteLine($"BookedEndDate: {booking.BookedEndDate}");
            Console.WriteLine($"CarId: {booking.CarId}");
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
            if (BookingDatesIntersect(booking))
            {
                ModelState.AddModelError("", "Sorry, this date for this car is already booked") ;
            }
            if (ModelState.IsValid)
            {
                _context.CarBookings.Add(booking);
                _context.SaveChanges();
                return RedirectToAction("Search", new { CarId = booking.CarId });
            }
            return View(booking);
        }
        public IActionResult Delete(int id)
        {
            var CarBooking = _context.CarBookings.FirstOrDefault(cb => cb.Id == id);

            if (CarBooking == null)
            {
                return NotFound();
            }

            var Car = _context.Cars.FirstOrDefault(c => c.CarId == CarBooking.CarId);
            if (Car == null)
            {
                return NotFound(); 
            }

            CarBooking.Car = Car;
            return View(CarBooking);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var CarBooking = _context.CarBookings.Find(id);


            if (CarBooking != null)
			{
                if (CarBooking.BookedStartDate < DateTime.Now.AddDays(1))
                {
                    ModelState.AddModelError("", "You cannot delete a booking within 24 hours of the start date.");
                    return View("Delete", CarBooking); 
                }

                _context.CarBookings.Remove(CarBooking);
				_context.SaveChanges();
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

                CarBookings = CarBookings.Where(c => c.CarId == CarId);
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
        private bool BookingDatesIntersect(CarBooking newBooking)
        {
            var existingBookings = _context.CarBookings
                .Where(b => b.CarId == newBooking.CarId && b.Id != newBooking.Id)
                .ToList();

            foreach (var existingBooking in existingBookings)
            {
                if (newBooking.BookedStartDate < existingBooking.BookedEndDate ||
                    newBooking.BookedEndDate > existingBooking.BookedStartDate)
                {
                    return true; 
                }
            }

            return false; 
        }

    }
}

