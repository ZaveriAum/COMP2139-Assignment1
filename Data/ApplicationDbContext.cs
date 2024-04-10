using COMP2139_Assignment1.Areas.NorthPole.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DbSet<CarBooking> CarBookings {  get; set; }
        public DbSet<RoomBooking> RoomBookings {  get; set; }
        public DbSet<FlightBooking> FlightBookings { get; set; }

    }
}
