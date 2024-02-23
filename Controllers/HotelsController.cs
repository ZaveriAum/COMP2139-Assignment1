using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

//update
namespace COMP2139_Assignment1.Controllers
{
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

        public IActionResult Create()
        {
            return View();
        }
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

        public IActionResult Details(int id)
        {
            var hotel = _db.Hotels.FirstOrDefault(p => p.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        public IActionResult Edit(int id)
        {
            var hotel = _db.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("HotelName", "HotelLocation", "Rating", "Description")] Hotel hotel)
        {
            if (id != hotel.HotelId)
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

        public IActionResult Delete(int id)
        {
            var project = _db.Hotels.FirstOrDefault(p => p.HotelId == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

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
        public async Task<IActionResult> Search(string searchName, string searchLocation, int searchRating)
        {
            var hotelQuery = from f in _db.Hotels select f;

            bool searchPerformed = !String.IsNullOrEmpty(searchName) || !String.IsNullOrEmpty(searchLocation);

            if (searchPerformed)
            {
                hotelQuery = hotelQuery.Where(f => f.HotelName.Contains(searchName) ||
                                                      f.HotelLocation.Contains(searchLocation));
            }
            var hotels = await hotelQuery.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchName"] = searchName;
            ViewData["seacrhLocation"] = searchLocation;
            return View("Search", hotels);
        }
    }
}

    


