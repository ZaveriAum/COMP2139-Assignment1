﻿using COMP2139_Assignment1.Areas.NorthPole.Models;
using COMP2139_Assignment1.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace COMP2139_Assignment1.Areas.NorthPole.Controllers
{
    [Area("NorthPole")]
    [Route("[area]/[controller]/[action]")]
    public class RoomBookingsController : Controller
    {
        private readonly UserManager<NorthPoleUser> _userManager;
        private readonly ApplicationDbContext _context;

        public RoomBookingsController(ApplicationDbContext context,UserManager<NorthPoleUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int RoomId)
        {
            ViewData["RoomId"] = RoomId;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = roomId;
            ViewData["Description"] = room.Description;
            ViewData["Price"] = room.Price;
            ViewData["HotelId"] = room.HotelId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("BookedStartDate", "BookedEndDate", "RoomId")] RoomBooking booking)
        {
            if (booking.BookedEndDate < booking.BookedStartDate)
            {
                ModelState.AddModelError("BookedEndDate", "End date must be equal or later than start date");
                return View(booking);
            }
            if (booking.BookedStartDate < DateTime.Now.AddDays(-1))
            {
                ModelState.AddModelError("BookedStartDate", "Start date cannot be earlier than today's date");
                return View(booking);
            }
            if (BookingDatesIntersect(booking))
            {
                ModelState.AddModelError("", "Sorry, this date for this car is already booked");
                return View(booking);
            }
            booking.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                await _context.RoomBookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Search", new { booking.RoomId });
            }
            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var booking = await _context.RoomBookings.Include(r => r.Room).FirstOrDefaultAsync(r => r.Id == Id);
            if (booking == null)
            {
                return NotFound();
            }
            var Room = await _context.Rooms.FindAsync(booking.RoomId);
            if (Room == null)
            {
                return NotFound();
            }
            ViewData["Description"] = Room.Description;
            ViewData["Price"] = Room.Price;
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id", "BookedStartDate", "BookedEndDate", "RoomId")] RoomBooking booking)
        {
            if (Id != booking.Id)
            {
                return NotFound();
            }
            var Room = await _context.Rooms.FindAsync(booking.RoomId);
            if (Room == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (booking.BookedEndDate < booking.BookedStartDate)
                {
                    ModelState.AddModelError("BookedEndDate", "End date must be equal or later than start date");
                    return View(booking);
                }
                if (booking.BookedStartDate < DateTime.Now.AddDays(-1))
                {
                    ModelState.AddModelError("BookedStartDate", "Start date cannot be earlier than today's date");
                    return View(booking);
                }
                if (BookingDatesIntersect(booking))
                {
                    ModelState.AddModelError("", "Sorry, this date for this car is already booked");
                    return View(booking);
                }
                _context.RoomBookings.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Search", new { booking.RoomId });
            }
            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var RoomBooking = await _context.RoomBookings.FirstOrDefaultAsync(rb => rb.Id == id);

            if (RoomBooking == null)
            {
                return NotFound();
            }
            var Room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == RoomBooking.RoomId);
            if (Room == null)
            {
                return NotFound();
            }
            RoomBooking.Room = Room;
            return View(RoomBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var RoomBooking = await _context.RoomBookings.FindAsync(id);

            if (RoomBooking != null)
            {
                if (RoomBooking.BookedStartDate < DateTime.Now.AddDays(1))
                {
                    ModelState.AddModelError("", "You cannot delete a booking within 24 houts of the start date.");
                    return View("Delete", RoomBooking);
                }
                _context.RoomBookings.Remove(RoomBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Search", new { roomId = RoomBooking.RoomId });
            }
            return NotFound();
        }

        public async Task<IActionResult> Search(int RoomId)
        {
            var Room = await _context.Rooms.FindAsync(RoomId);
            if (Room == null)
            {
                return NotFound();
            }
            var RoomBookings = from rb in _context.RoomBookings select rb;
            RoomBookings = RoomBookings.Where(c => c.RoomId == RoomId);
            ViewData["RoomId"] = RoomId;
            ViewData["Description"] = Room.Description;
            ViewData["Price"] = Room.Price;

            var RoomBookingList = await RoomBookings.ToListAsync();
            return View("Index", RoomBookingList);
        }

        private bool BookingDatesIntersect(RoomBooking newBooking)
        {
            var existingBookings = _context.RoomBookings
                .Where(b => b.RoomId == newBooking.RoomId && b.Id != newBooking.Id)
                .ToList();

            foreach (var existingBooking in existingBookings)
            {
                if (newBooking.BookedStartDate <= existingBooking.BookedEndDate && newBooking.BookedStartDate >= existingBooking.BookedStartDate ||
                    newBooking.BookedEndDate >= existingBooking.BookedStartDate && newBooking.BookedEndDate <= existingBooking.BookedEndDate)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
