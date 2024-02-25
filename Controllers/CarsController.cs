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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
                return RedirectToAction("Details" ,new {carId=car.CarId});
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
        public IActionResult Edit(int id, [Bind("CarId","PlateNumber","Brand","Model","City","Price","RentalCompany" ,"Description", "PickUpLocation")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingCar = _context.Cars.Find(id);
                if (existingCar != null)
                {
                    return NotFound();
                }
                _context.Cars.Update(car);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }


        public IActionResult Delete(int Carid)
        {
            var car = _context.Cars.FirstOrDefault(p => p.CarId == Carid);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int CarId)
        {
            var car = _context.Cars.Find(CarId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
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
           
           // string SearchString =  City+ "&Brand="+ Brand + "&Model=" + Model +"&MinPrice="+MinPrice+"&MaxPrice=" + MaxPrice;

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

            //TempData["SearchString"] = SearchString;

            return View(await Cars.ToListAsync());
        }


    }
}
