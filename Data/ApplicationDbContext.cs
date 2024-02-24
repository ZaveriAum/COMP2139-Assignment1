using COMP2139_Assignment1.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DbSet<ReviewCar> ReviewCars { get; set; }
        public DbSet<ReviewHotel> ReviewHotels { get; set; }
        public DbSet<ReviewRoom> ReviewRooms { get; set; }

        public DbSet<PhotoCar> PhotoCars { get; set; }
        public DbSet<PhotoHotel> PhotoHotels { get; set; }
        public DbSet<PhotoRoom> PhotoRooms { get; set; }
        public DbSet<CarBooking> CarBookings {  get; set; }
        public DbSet<RoomBooking> RoomBookings {  get; set; }
        public DbSet<FlightBooking> FlightBookings { get; set; }

    }
}
