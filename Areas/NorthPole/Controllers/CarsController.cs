using COMP2139_Assignment1.Areas.NorthPole.Models;
using COMP2139_Assignment1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace COMP2139_Assignment1.Controllers
{
    [Area("NorthPole")]
    [Route("[area]/[controller]/[action]")]
    public class CarsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.ToListAsync();
            return View(cars);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int carId)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int CarId, [Bind("CarId","PlateNumber","Brand","Model","City","Price","MaxPassenger","RentalCompany" ,"Description", "PickUpLocation")] Car car)
        {
            if (CarId != car.CarId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!await CarExists(car.CarId))
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

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Delete(int Carid)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(p => p.CarId == Carid);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CarId)
        {
            var car = _context.Cars.Find(CarId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


        private async Task<bool> CarExists(int carId)
        {
            return await _context.Cars.AnyAsync(c=> c.CarId == carId);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string City,string Model,string Brand,double MinPrice,double MaxPrice, int NumPassenger)
        {
            if (_context.Cars == null)
            {
                return Problem("Sorry, there are currently no cars available at the moment!");
            }
           
           // string SearchString =  City+ "&Brand="+ Brand + "&Model=" + Model +"&MinPrice="+MinPrice+"&MaxPrice=" + MaxPrice;

            var Cars= from c in _context.Cars
                         select c;
            if(NumPassenger > 0)
            {
                Cars = Cars.Where(s=>s.MaxPassenger >= NumPassenger);
            }

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
