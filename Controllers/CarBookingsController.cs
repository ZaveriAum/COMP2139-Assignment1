  using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create(int carId)
        {
            var car = _context.Cars.Find(carId);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["carId"] = carId;
            ViewData["PlateNumber"] = car.PlateNumber;
            ViewData["City"] = car.City;
            ViewData["PickupLocation"] = car.PickUpLocation;
            ViewData["Brand"] = car.Brand;
            ViewData["Model"] = car.Model;
            ViewData["Price"]= car.Price;
            ViewData["RentalCompany"] = car.RentalCompany;
            ViewData["SearchString"] = TempData["SearchString"];

            return View();
        }
        public IActionResult Create([Bind("BookedStartDate", "BookedEndDate", "CarId")] CarBooking booking)
        {
            Console.WriteLine(booking.BookedEndDate);
            if (ModelState.IsValid)
            {

                _context.CarBookings.Add(booking);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }
    }
}
