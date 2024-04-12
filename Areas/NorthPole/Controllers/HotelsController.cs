using COMP2139_Assignment1.Areas.NorthPole.Models;
using COMP2139_Assignment1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

//update
namespace COMP2139_Assignment1.Areas.NorthPole.Controllers
{
    [Area("NorthPole")]
    [Route("[area]/[controller]/[action]")]
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HotelsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult List()
        {


            return View(_db.Hotels.ToList());
        }

        public IActionResult Index()
        {


            return View(_db.Hotels.ToList());
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _db.Hotels.Add(hotel);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        [HttpGet]
        public IActionResult Details(int hotelId)
        {
            var hotel = _db.Hotels.FirstOrDefault(p => p.HotelId == hotelId);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
        public IActionResult Edit(int hotelId)
        {
            var hotel = _db.Hotels.Find(hotelId);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int HotelId, [Bind("HotelId", "HotelName", "City", "HotelLocation", "Description")] Hotel hotel)
        {
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
        }

        private bool HotelExists(int hotelId)
        {
            return _db.Hotels.Any(e => e.HotelId == hotelId);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int HotelId)
        {
            var hotel = _db.Hotels.Find(HotelId);
            if (hotel != null)
            {
                _db.Hotels.Remove(hotel);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> Search(string Name, string City)
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
        }
    }
}




