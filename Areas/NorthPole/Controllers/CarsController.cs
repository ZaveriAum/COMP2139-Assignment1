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
        private readonly ILogger<CarsController> _logger;

        public CarsController(ApplicationDbContext context, ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Search Page for available cars.");
            try
            {
                var cars = await _context.Cars.ToListAsync();
                return View(cars);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            _logger.LogInformation("Create page for car");
            try {
                return View();
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            _logger.LogInformation("Create a Car entity");
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.Cars.AddAsync(car);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(car);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("Create/{carId:int}")]
        public async Task<IActionResult> Details(int carId)
        {
            _logger.LogInformation("Details page for car details.");
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == carId);
                if (car == null)
                {
                    return NotFound();
                }
                return View(car);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet("Create/{carId:int}")]
        public async Task<IActionResult> Edit(int carId)
        {
            _logger.LogInformation("Edit page for car entity");
            try
            {
                var car = await _context.Cars.FindAsync(carId);
                if (car == null)
                {
                    return NotFound();
                }
                return View(car);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("Create/{CarId:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int CarId, [Bind("CarId", "PlateNumber", "Brand", "Model", "City", "Price", "MaxPassenger", "RentalCompany", "Description", "PickUpLocation")] Car car)
        {
            _logger.LogInformation("Edit function for car information.");
            try {
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
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await CarExists(car.CarId))
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("Create/{CarId:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int Carid)
        {
            _logger.LogInformation("Delete page for car entity.");
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(p => p.CarId == Carid);
                if (car == null)
                {
                    return NotFound();
                }
                return View(car);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("Create/{CarId:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CarId)
        {
            _logger.LogInformation("Delete function for the deleting the car entity.");
            try
            {
                var car = _context.Cars.Find(CarId);
                if (car != null)
                {
                    _context.Cars.Remove(car);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        private async Task<bool> CarExists(int carId)
        {
            _logger.LogInformation("Check if the car exists");
            try {
                return await _context.Cars.AnyAsync(c => c.CarId == carId);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpGet("Search/{City?}/{Model?}/{Brand?}/{MinPrice:double?}/{MaxPrice:double?}/{NumPassenger:int?}")]
        public async Task<IActionResult> Search(string City, string Model, string Brand, double MinPrice, double MaxPrice, int NumPassenger)
        {
            _logger.LogInformation("Seach the car for booking based on the user requirements");
            try {
                if (_context.Cars == null)
                {
                    return Problem("Sorry, there are currently no cars available at the moment!");
                }

                // string SearchString =  City+ "&Brand="+ Brand + "&Model=" + Model +"&MinPrice="+MinPrice+"&MaxPrice=" + MaxPrice;

                var Cars = from c in _context.Cars
                           select c;
                if (NumPassenger > 0)
                {
                    Cars = Cars.Where(s => s.MaxPassenger >= NumPassenger);
                }

                if (!String.IsNullOrEmpty(City))
                {
                    Cars = Cars.Where(s => s.City!.Contains(City));
                }
                if (!String.IsNullOrEmpty(Model))
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
            }catch(Exception ex) {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
