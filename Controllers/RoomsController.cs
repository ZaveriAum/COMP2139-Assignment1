 using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace COMP2139_Assignment1.Controllers
{
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Description", "Price","Rating","HotelId")] Room room)
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
        public IActionResult Edit(int id)
        {
            var room = _context.Rooms.Include(t => t.Hotel).FirstOrDefault(t => t.HotelId == id);
            if (room == null)
            {
                return NotFound();
            }
            ViewBag.Projects = new SelectList(_context.Hotels, "ProjectId", "Name", room.HotelId);
            return View(room);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RoomId", "Description","Price","Rating", "HotelId")] Room room)
        {
            if (id != room.RoomId)
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
        public IActionResult Delete(int id)
        {
            var room = _context.Rooms.Include(t => t.Hotel).FirstOrDefault(t => t.HotelId == id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

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

    }
}
