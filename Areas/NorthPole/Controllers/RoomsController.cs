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
        private readonly ILogger<RoomsController> _logger;

        public RoomsController(ApplicationDbContext context, ILogger<RoomsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int hotelId)
        {
            _logger.LogInformation($"List of room for hotel: {hotelId}.");
            try {
                var rooms = await _context.Rooms.Where(f => f.HotelId == hotelId).ToListAsync();
                ViewBag.HotelId = hotelId;
                return View(rooms);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation($"Details page for rooms with room id: {id}");
            try
            {
                var room = await _context.Rooms.Include(f => f.Hotel).FirstOrDefaultAsync(f => f.RoomId == id);
                if (room == null)
                {
                    return NotFound();
                }
                return View(room);
            }catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
        public async Task<IActionResult> Create(int hotelId)
        {
            _logger.LogInformation($"Create page for hotel with id: {hotelId}");
            try {
                var hotel = await _context.Hotels.FindAsync(hotelId);
                if (hotel == null)
                {
                    return NotFound();
                }

                var room = new Room
                {
                    HotelId = hotelId
                };
                return View(room);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description", "Price", "MaxGuest", "HotelId")] Room room)
        {
            _logger.LogInformation($"Create page for Room with: hotel id: {room.HotelId}, room price: {room.Price}, room max guest: {room.MaxGuest} ,room description: {room.Description}");
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.Rooms.AddAsync(room);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { hotelId = room.HotelId });
                }
                ViewBag.Hotels = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
                return View(room);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Edit(int hotelId)
        {
            _logger.LogInformation($"Edit page for the room id: {hotelId}");
            try
            {
                var room = await _context.Rooms.Include(t => t.Hotel).FirstOrDefaultAsync(t => t.HotelId == hotelId);
                if (room == null)
                {
                    return NotFound();
                }
                return View(room);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Edit(int roomId, [Bind("RoomId", "Description", "Price", "MaxGuest", "HotelId")] Room room)
        {
            _logger.LogInformation($"Edit page for room with info: room id: {room.RoomId}, room price: {room.Price}, hotel id: {room.HotelId}.");
            try {
                if (roomId != room.RoomId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { hotelId = room.HotelId });
                }
                ViewBag.Projects = new SelectList(_context.Hotels, "ProjectId", "HotelName", room.HotelId);
                return View(room);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int roomId)
        {
            _logger.LogInformation($"Delete room with room id: {roomId}.");
            try
            {
                var room = await _context.Rooms.Include(t => t.Hotel).FirstOrDefaultAsync(t => t.RoomId == roomId);
                if (room == null)
                {
                    return NotFound();
                }
                return View(room);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int roomId)
        {
            _logger.LogInformation($"Delete for room id: {roomId}.");
            try
            {
                var room = await _context.Rooms.FindAsync(roomId);
                if (room != null)
                {
                    _context.Rooms.Remove(room);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { hotelId = room.HotelId });
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

        public async Task<IActionResult> Search(double MinPrice, double MaxPrice, int NumGuest)
        {
            _logger.LogInformation($"Search room: min price: {MinPrice}, max price: {MaxPrice}, number of guest: {NumGuest}.");
            try {
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }
    }
}
