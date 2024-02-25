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
        public IActionResult Create([Bind("BookedStartDate", "BookedEndDate", "RoomId")]RoomBooking booking) 
        {
            Console.WriteLine($"BookedStartDate: {booking.BookedStartDate}");
            Console.WriteLine($"BookedEndDate: {booking.BookedEndDate}");
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
			if (ModelState.IsValid)
            {
                _context.RoomBookings.Add(booking);
                _context.SaveChanges();
                return RedirectToAction("Search", new {RoomId = booking.RoomId });
            }
            return View(booking);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var booking = _context.RoomBookings.Include(r => r.Room).FirstOrDefault(r=>r.Id == Id);
            if(booking == null)
            {
                return NotFound();
            }
            var Room = _context.Rooms.Find(booking.RoomId);
            if (Room == null)
            {
                return NotFound();
            }
            ViewData["Description"] = Room.Description;
            ViewData["Price"] = Room.Price;
            ViewData["Rating"] = Room.Rating;
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind("Id", "BookedStartDate", "BookedEndDate", "RoomId")]RoomBooking booking)
        {
            if(Id != booking.Id)
            {
                return NotFound();
            }
            var Room = _context.Rooms.Find(booking.RoomId);
            if ( Room == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
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
				_context.SaveChanges();
				return RedirectToAction("Search", new { RoomId = booking.RoomId });
			}
			return View(booking);
		}

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var RoomBooking = _context.RoomBookings.FirstOrDefault(rb => rb.Id == id);

            if (RoomBooking == null)
            {
                return NotFound();
            }
            var Room = _context.Rooms.FirstOrDefault(r => r.RoomId == RoomBooking.RoomId);
            if (Room == null)
            {
                return NotFound();
            }
            RoomBooking.Room = Room;
            return View(RoomBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) 
        {
            var RoomBooking = _context.RoomBookings.Find(id);

            if (RoomBooking != null)
            {
                if (RoomBooking.BookedStartDate < DateTime.Now.AddDays(1))
                {
                    ModelState.AddModelError("", "You cannot delete a booking within 24 houts of the start date.");
                    return View("Delete", RoomBooking);
                }
                _context.RoomBookings.Remove(RoomBooking);
                _context.SaveChanges();
                return RedirectToAction("Search", new {roomId = RoomBooking.RoomId});
            }
            return NotFound();
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

		private bool BookingDatesIntersect(RoomBooking newBooking)
		{
			var existingBookings = _context.RoomBookings
				.Where(b => b.RoomId == newBooking.RoomId && b.Id != newBooking.Id)
				.ToList();

			foreach (var existingBooking in existingBookings)
			{
				if ((newBooking.BookedStartDate <= existingBooking.BookedEndDate && newBooking.BookedStartDate >= existingBooking.BookedStartDate) ||
                    (newBooking.BookedEndDate >= existingBooking.BookedStartDate) && (newBooking.BookedEndDate <= existingBooking.BookedEndDate))
                {
					return true;
				}
			}

			return false;
		}
	}
}
