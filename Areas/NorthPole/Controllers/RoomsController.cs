using COMP2139_Assignment1.Areas.NorthPole.Models;
using COMP2139_Assignment1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace COMP2139_Assignment1.Areas.NorthPole.Controllers
{
    [Area("NorthPole")]
    [Route("[area]/[controller]/[action]")]
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(int hotelId)
        {
            var rooms = _context.Rooms.Where(f => f.HotelId == hotelId).ToList();
            ViewBag.HotelId = hotelId;
            return View(rooms);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var room = _context.Rooms.Include(f => f.Hotel).FirstOrDefault(f => f.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
        public IActionResult Create(int hotelId)
        {
            var hotel = _context.Hotels.Find(hotelId);
            if (hotel == null)
            {
                return NotFound();
            }

            var room = new Room
            {
                HotelId = hotelId
            };
            return View(room);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Description", "Price", "MaxGuest", "HotelId")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { hotelId = room.HotelId });
            }
            ViewBag.Hotels = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
            return View(room);
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Edit(int hotelId)
        {
            var room = _context.Rooms.Include(t => t.Hotel).FirstOrDefault(t => t.HotelId == hotelId);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Edit(int roomId, [Bind("RoomId", "Description", "Price", "MaxGuest", "HotelId")] Room room)
        {
            if (roomId != room.RoomId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Update(room);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { hotelId = room.HotelId });
            }
            ViewBag.Projects = new SelectList(_context.Hotels, "ProjectId", "HotelName", room.HotelId);
            return View(room);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Delete(int id)
        {
            var room = _context.Rooms.Include(t => t.Hotel).FirstOrDefault(t => t.HotelId == id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int roomId)
        {
            var room = _context.Rooms.Find(roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { hotelId = room.HotelId });
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Search(double MinPrice, double MaxPrice, int NumGuest)
        {

            if (_context.Rooms == null)
            {
                return Problem("Sorry, there are currently no cars available at the moment!");
            }
            var Rooms = from r in _context.Rooms select r;
            if (NumGuest > 0)
            {
                Rooms = Rooms.Where(c => c.MaxGuest >= NumGuest);
            }
            if (MinPrice > 0)
            {
                Rooms = Rooms.Where(c => c.Price >= MinPrice);
            }
            if (MaxPrice > 0)
            {
                Rooms = Rooms.Where(c => c.Price <= MaxPrice);
            }
            return View("Index", await Rooms.ToListAsync());
        }
    }
}
