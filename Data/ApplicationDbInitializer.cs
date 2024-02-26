using COMP2139_Assignment1.Models;
using System.Drawing.Drawing2D;

namespace COMP2139_Assignment1.Data
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                if (!context.Cars.Any())
                {
                    context.Cars.AddRange(new List<Car>()
                    {
                        new Car()
                        {
                            PlateNumber = "T3ST PLAT3",
                            City = "Mississauga",
                            Brand = "Volswagen",
                            Model = "Vento",
                            MaxPassenger = 5,
                            Description = "Brand new Volswagen Vento for a cheap price!",
                            Price = 49.99,
                            RentalCompany =  "Cheap Car Rentals",
                            PickUpLocation =  "898 Central Pkwy"
                        },
                        new Car()
                        {
                            PlateNumber= "C4R 123",
                            City= "Toronto",
                            Brand= "Toyota",
                            Model= "Camry",
                            MaxPassenger = 1,
                            Description= "Comfortable Toyota Camry, perfect for city drives.",
                            Price= 55.99,
                            RentalCompany= "City Cars",
                            PickUpLocation= "123 Main St" 
                        },
                        new Car()
                        {
                            PlateNumber= "VAN 567",
                            City= "Vancouver",
                            Brand= "Honda",
                            Model= "Civic",
                            MaxPassenger = 4,
                            Description= "Economical Honda Civic for your travel needs.",
                            Price= 48.50,
                            RentalCompany= "West Coast Rentals",
                            PickUpLocation= "456 Ocean Ave"
                        },
                        new Car(){
                            PlateNumber= "MTL 789",
                            City= "Montreal",
                            Brand= "Ford",
                            Model= "Escape",
                            MaxPassenger = 3,
                            Description= "Ford Escape - A reliable SUV for your adventures.",
                            Price= 62.75,
                            RentalCompany= "Maple Leaf Rentals",
                            PickUpLocation= "789 Mountain Rd"
                        },
                        new Car()
                        {
                          PlateNumber= "CAL 321",
                          City= "Calgary",
                          Brand= "Chevrolet",
                          Model= "Malibu",
                          MaxPassenger = 6,
                          Description= "Chevy Malibu - A stylish choice for your road trip.",
                          Price= 54.25,
                          RentalCompany= "Rocky Mountain Cars",
                          PickUpLocation= "321 Pine St"
                        }
                    });
                    context.SaveChanges();
                }
                if (!context.CarBookings.Any())
                {
                    context.CarBookings.AddRange(new List<CarBooking>()
                    {
                        new CarBooking()
                        {
                            BookedStartDate = DateTime.Now,
                            BookedEndDate= DateTime.Now.AddDays(4),
                            CarId = 1 
                        }, 
                        new CarBooking()
                        {
                            BookedStartDate = DateTime.Now.AddDays(4),
                            BookedEndDate= DateTime.Now.AddDays(5),
                            CarId = 1
                        },
                        new CarBooking()
                        {
                            BookedStartDate = DateTime.Now.AddDays(5),
                            BookedEndDate= DateTime.Now.AddDays(7),
                            CarId = 2
                        },
                        new CarBooking()
                        {
                            BookedStartDate = DateTime.Now.AddDays(4),
                            BookedEndDate= DateTime.Now.AddDays(6),
                            CarId = 2
                        },
                        new CarBooking
                        {
                            BookedStartDate = DateTime.Now.AddDays(3),
                            BookedEndDate = DateTime.Now.AddDays(8),
                            CarId = 3
                        },
                        new CarBooking
                        {   
                        BookedStartDate = DateTime.Now.AddDays(6),
                        BookedEndDate = DateTime.Now.AddDays(9),
                        CarId = 4
                        },
                         new CarBooking
                         {
                        BookedStartDate = DateTime.Now.AddDays(2),
                        BookedEndDate = DateTime.Now.AddDays(7),
                        CarId = 5
                        },

                });
                    context.SaveChanges();
                }
                if (!context.Flights.Any())
                {
                    context.Flights.AddRange(new List<Flight>()
                    {
                        new Flight()
                        {
                            FlightNumber = "AB112",
                            Airline = "Canada Air",
                            DepartureDate = new DateOnly(2024, 8, 8),
                            DepartureTime = new TimeOnly(12, 30, 0),
                            ArrivalDate = new DateOnly(2024, 8, 10),
                            ArrivalTime = new TimeOnly(15, 45, 0),
                            Price = 49.99,
                            From =  "Toronto",
                            To =  "Quebec",
                            Seats = 10
                        },
                        new Flight()
                        {
                            FlightNumber = "CD223",
                            Airline = "Maple Airlines",
                            DepartureDate = new DateOnly(2024, 8, 15),
                            DepartureTime = new TimeOnly(14, 0, 0),
                            ArrivalDate = new DateOnly(2024, 8, 17),
                            ArrivalTime = new TimeOnly(17, 30, 0),
                            Price = 79.99,
                            From = "Vancouver",
                            To = "Montreal",
                            Seats = 15
                        },
                        new Flight
                        {
                            FlightNumber = "EF324",
                            Airline = "Polar Express",
                            DepartureDate = new DateOnly(2024, 8, 20),
                            DepartureTime = new TimeOnly(9, 45, 0),
                            ArrivalDate = new DateOnly(2024, 8, 22),
                            ArrivalTime = new TimeOnly(12, 15, 0),
                            Price = 64.99,
                            From = "Calgary",
                            To = "Halifax",
                            Seats = 20
                        },
                        new Flight
                        {
                            FlightNumber = "GH445",
                            Airline = "Northern Lights Airways",
                            DepartureDate = new DateOnly(2024, 8, 25),
                            DepartureTime = new TimeOnly(18, 0, 0),
                            ArrivalDate = new DateOnly(2024, 8, 27),
                            ArrivalTime = new TimeOnly(21, 30, 0),
                            Price = 89.99,
                            From = "Edmonton",
                            To = "Winnipeg",
                            Seats = 25
                        },
                        new Flight
                        {
                            FlightNumber = "IJ556",
                            Airline = "Rocky Mountain Flights",
                            DepartureDate = new DateOnly(2023, 9, 1),
                            DepartureTime = new TimeOnly(10, 30, 0),
                            ArrivalDate = new DateOnly(2023, 9, 3),
                            ArrivalTime = new TimeOnly(13, 45, 0),
                            Price = 54.99,
                            From = "Banff",
                            To = "Saskatoon",
                            Seats = 30
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
