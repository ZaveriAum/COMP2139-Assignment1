using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP2139_Assignment1.Controllers
{
    public class RoomBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
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
            ViewData["roomId"] = roomId;
            ViewData["Description"] = room.Description;
            ViewData["Price"] = room.Price;
            ViewData["Rating"] = room.Rating;
            ViewData["HotelId"] = room.HotelId;
            ViewData["Hotel"] = room.Hotel;
            return View(room);
        }

        [HttpPost]
        public IActionResult Create([Bind("BookingStartDate", "BookingEndDate", "roomId")]RoomBooking roomBooking) 
        {
            if(ModelState.IsValid)
            {
                _context.RoomBookings.Add(roomBooking);
                _context.SaveChanges();
                return RedirectToAction("Details");
            }
            return View("/Home/Index");
        }
    }
}
