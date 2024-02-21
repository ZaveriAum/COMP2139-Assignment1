using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
<<<<<<< Updated upstream
using Microsoft.EntityFrameworkCore;
using System;

namespace COMP2139_Labs.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProjectsController(ApplicationDbContext db)
=======
using System;

namespace COMP2139_Assignment1.Controllers
{
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HotelsController(ApplicationDbContext db)
>>>>>>> Stashed changes
        {
            _db = db;
        }

        public IActionResult Index()
        {
<<<<<<< Updated upstream
            /*var projects = new List<Project>()
            {
                new Project { ProjectId = "1", Name = "First 1", Description = "First Project" },
                new Project { ProjectId = "2", Name = "Second 2", Description = "Second Project" }
            };*/
=======
>>>>>>> Stashed changes

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
            var project = _db.Hotels.FirstOrDefault(p => p.HotelId == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
<<<<<<< Updated upstream

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
        public IActionResult Edit(int id, [Bind("ProjectId, Name, Description")] Hotel hotel)
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
                    if (!ProjectExists(hotel.HotelId))
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

        private bool ProjectExists(int projectId)
        {
            return _db.Hotels.Any(e => e.HotelId == projectId);
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
    }
}
=======
    }
}

>>>>>>> Stashed changes
