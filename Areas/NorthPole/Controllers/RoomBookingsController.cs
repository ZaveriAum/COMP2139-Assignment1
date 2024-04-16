using COMP2139_Assignment1.Areas.NorthPole.Models;
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
        private readonly ILogger<RoomBookingsController> _logger;

        public RoomBookingsController(ApplicationDbContext context,UserManager<NorthPoleUser> userManager, ILogger<RoomBookingsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("Index/{RoomId:int}")]
        public async Task<IActionResult> Index(int RoomId)
        {
            _logger.LogInformation($"Room Booking page for roomId: {RoomId}");
            try {
                ViewData["RoomId"] = RoomId;
                return View();
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet("Create/{roomId:int}")]
        public async Task<IActionResult> Create(int roomId)
        {
            _logger.LogInformation($"Create booking page for roomId: {roomId}");
            try {
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([Bind("BookedStartDate", "BookedEndDate", "RoomId")] RoomBooking booking)
        {
            _logger.LogInformation($"Create room booking for car booking: {booking.BookedStartDate} : {booking.BookedEndDate} with room Id: {booking.RoomId}");
            try {
                booking.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
                if (await BookingDatesIntersect(booking))
                {
                    ModelState.AddModelError("", "Sorry, this date for this car is already booked");
                    return View(booking);
                }
                if (ModelState.IsValid)
                {
                    await _context.RoomBookings.AddAsync(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Search", new { booking.RoomId });
                }
                return View(booking);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet("Edit/{Id:int}")]
        public async Task<IActionResult> Edit(int Id)
        {
            _logger.LogInformation($"Edit page the car id with carId: {Id}");
            try {
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost("Edit/{Id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id", "BookedStartDate", "BookedEndDate", "RoomId")] RoomBooking booking)
        {
            _logger.LogInformation($"Edit for booked room with: {booking.Id}, {booking.BookedStartDate}, {booking.BookedEndDate}, {booking.RoomId}.");
            try {
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
                    if (await BookingDatesIntersect(booking))
                    {
                        ModelState.AddModelError("", "Sorry, this date for this car is already booked");
                        return View(booking);
                    }
                    _context.RoomBookings.Update(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Search", new { booking.RoomId });
                }
                return View(booking);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }
        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Delete page for room booking controller: {id}.");
            try {
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpPost("DeleteConfirmed/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation($"Delete confirmed for room booking with id: {id}.");
            try {
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        [HttpGet("Search/{RoomId:int}")]
        public async Task<IActionResult> Search(int RoomId)
        {
            _logger.LogInformation($"Searching for room booking with id: {RoomId}");
            try {
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
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        private async Task<bool> BookingDatesIntersect(RoomBooking newBooking)
        {
            _logger.LogInformation($"Check if the date is available for room booking: {newBooking}");
            try
            {
                var existingBookings = await _context.RoomBookings
                    .Where(b => b.RoomId == newBooking.RoomId && b.Id != newBooking.Id)
                    .ToListAsync();

                foreach (var existingBooking in existingBookings)
                {
                    if (newBooking.BookedStartDate <= existingBooking.BookedEndDate && newBooking.BookedStartDate >= existingBooking.BookedStartDate ||
                        newBooking.BookedEndDate >= existingBooking.BookedStartDate && newBooking.BookedEndDate <= existingBooking.BookedEndDate)
                    {
                        return true;
                    }
                }

                return false;
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
