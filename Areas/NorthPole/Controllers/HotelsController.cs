using COMP2139_Assignment1.Areas.NorthPole.Models;
using COMP2139_Assignment1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;

//update
namespace COMP2139_Assignment1.Areas.NorthPole.Controllers
{
    [Area("NorthPole")]
    [Route("[area]/[controller]/[action]")]
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HotelsController> _logger;

        public HotelsController(ApplicationDbContext db, ILogger<HotelsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            _logger.LogInformation("List the hotels");
            try
            {
                return View(await _db.Hotels.ToListAsync());
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Calling the list of hotels.");
            try {
                var hotels = await _db.Hotels.ToListAsync();
                return View(hotels);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Create()
        {
            _logger.LogInformation("Create page for hotel entity.");
            try {
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
        public async Task<IActionResult> Create(Hotel hotel)
        {
            _logger.LogInformation("Create page for hotel entity.");
            try {
                if (ModelState.IsValid)
                {
                    await _db.Hotels.AddAsync(hotel);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(hotel);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet("{hotelName}")]
        public async Task<IActionResult> Details(int hotelId, string hotelName)
        {
            _logger.LogInformation("Details page for hotel entity.");
            try
            {
                var hotel = await _db.Hotels.FirstOrDefaultAsync(p => p.HotelId == hotelId);
                if (hotel == null)
                {
                    return NotFound();
                }

                return View(hotel);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }



        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int hotelId)
        {
            _logger.LogInformation($"Edit page for hotel with hotel id: {hotelId}");
            try {
                var hotel = await _db.Hotels.FindAsync(hotelId);
                if (hotel == null)
                {
                    return NotFound();
                }
                return View(hotel);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int HotelId, [Bind("HotelId", "HotelName", "City", "HotelLocation", "Description")] Hotel hotel)
        {
            _logger.LogInformation($"Edit page for hotel entity for hotel id: {HotelId}.");
            try {
                if (HotelId != hotel.HotelId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _db.Update(hotel);
                        _db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!HotelExists(hotel.HotelId))
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
                return View(hotel);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }
        public bool HotelExists(int hotelId)
        {
            _logger.LogInformation($"Check if the hotel exists with the id: {hotelId}.");
            try {
                return _db.Hotels.Any(e => e.HotelId == hotelId);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int hotelId)
        {
            _logger.LogInformation($"Delete page for hotel with id: {hotelId}");
            try {
                var hotel = await _db.Hotels.FindAsync(hotelId);
                if (hotel == null)
                {
                    return NotFound();
                }
                return View(hotel);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int hotelId)
        {
            _logger.LogInformation($"Delete function to hotel with hotel id: {hotelId}");
            try {
                var hotel = await _db.Hotels.FindAsync(hotelId);
                if (hotel != null)
                {
                    _db.Hotels.Remove(hotel);
                    _db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search(string Name, string City)
        {
            _logger.LogInformation($"Search for Hotel base on name: {Name}, city: {City}.");
            try
            {
                var hotelQuery = from f in _db.Hotels select f;

                bool searchPerformed = !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(City);

                if (searchPerformed)
                {
                    hotelQuery = hotelQuery.Where(f => f.HotelName.Contains(Name) ||
                                                          f.City.Contains(City));
                }
                var hotels = await hotelQuery.ToListAsync();
                return View("Search", hotels);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }
    }
}




