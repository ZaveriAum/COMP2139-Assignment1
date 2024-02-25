using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Controllers
{
    public class RoomBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int RoomId)
        {
            ViewData["RoomId"] = RoomId;
            return View();
        }

        [HttpGet]
        public IActionResult Create(int roomId)
        {
            var room = _context.Rooms.Find(roomId);
            if(room == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = roomId;
            ViewData["Description"] = room.Description;
            ViewData["Price"] = room.Price;
            ViewData["Rating"] = room.Rating;
            ViewData["HotelId"] = room.HotelId;
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("BookedStartDate", "BookedEndDate", "RoomId")]RoomBooking roomBooking) 
        {
            Console.WriteLine($"BookedStartDate: {roomBooking.BookedStartDate}");
            Console.WriteLine($"BookedEndDate: {roomBooking.BookedEndDate}");
            if (roomBooking.BookedEndDate < roomBooking.BookedStartDate)
            {
                ModelState.AddModelError("BookedEndDate", "End date must be equal or later than start date");
                return View(roomBooking);
            }
            if (roomBooking.BookedStartDate < DateTime.Now.AddDays(-1))
            {
                ModelState.AddModelError("BookedStartDate", "Start date cannot be earlier than today's date");
                return View(roomBooking);
            }
            if (ModelState.IsValid)
            {
                _context.RoomBookings.Add(roomBooking);
                _context.SaveChanges();
                return RedirectToAction("Search", new {RoomId = roomBooking.RoomId });
            }
            return View(roomBooking);
        }


        public async Task<IActionResult> Search(int RoomId)
        {
            var Room = await _context.Rooms.FindAsync(RoomId);
            if (Room == null) {
                return NotFound();
            }
            var  RoomBookings = from rb in _context.RoomBookings select rb;
            RoomBookings = RoomBookings.Where(c => c.RoomId == RoomId);
            ViewData["RoomId"] = RoomId;
            ViewData["Description"] = Room.Description;
            ViewData["Price"] = Room.Price;
            ViewData["Rating"] = Room.Rating;

            var RoomBookingList = await RoomBookings.ToListAsync();
            return View("Index", RoomBookingList);
        }
    }
}
