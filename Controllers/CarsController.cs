using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Cars.ToList());
        }

        [HttpGet]
        public IActionResult SearchCar()
        {
            return View(_context.Cars.ToList());
        }

        [HttpGet("ListCar")]
        public IActionResult ListCar()
        {
            return View();
        }

        [HttpPost("ListCar")]
        [ValidateAntiForgeryToken]
        public IActionResult ListCar(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        [HttpGet]
        public IActionResult Details(int carId)
        {
            var car = _context.Cars.FirstOrDefault(c => c.CarId == carId);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpGet]
        public IActionResult Edit(int carId)
        {
            var car = _context.Cars.Find(carId);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int carId, [Bind("PlateNumber", "City", "PickUpLocation", "Make", "Model", "Description", "Price", "RentalCompany")] Car car)
        {
            if(carId != car.CarId) { return NotFound(); }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    _context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!CarExists(car.CarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(car);
        }

        private bool CarExists(int carId)
        {
            return _context.Cars.Any(c=> c.CarId == carId);
        }

        /*        [HttpGet]
        public async Task<IActionResult> Search(string searchStringPickUp, DateOnly searchStringDate)
        {
            var carsQuery = from c in _context.Cars select c;
            bool searchPerformed = !String.IsNullOrEmpty(searchStringPickUp)
        }*/


    }
}
