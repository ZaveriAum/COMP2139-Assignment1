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
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
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

        [HttpGet]
        public async Task<IActionResult> Search(string City,string Model,string Brand,double MinPrice,double MaxPrice)
        {
            if (_context.Cars == null)
            {
                return Problem("Sorry, there are currently no cars availible at the moment!");
            }

            var Cars= from c in _context.Cars
                         select c;

            if (!String.IsNullOrEmpty(City))
            {
                Cars = Cars.Where(s => s.City!.Contains(City));
            }
            if(!String.IsNullOrEmpty(Model))
            {
                Cars = Cars.Where(s => s.Model.Contains(Model));
            }
            if (!String.IsNullOrEmpty(Brand))
            {
                Cars = Cars.Where(s => s.Brand.Contains(Brand));
            }
            if (MinPrice > 0)
            {
                Cars = Cars.Where(s => s.Price >= MinPrice);
            }

            if (MaxPrice > 0)
            {
                Cars = Cars.Where(s => s.Price <= MaxPrice);
            }



            return View(await Cars.ToListAsync());
        }


    }
}
