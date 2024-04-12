using COMP2139_Assignment1.Areas.NorthPole.Models;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Drawing2D;

namespace COMP2139_Assignment1.Data
{
    public class ApplicationDbInitializer
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder, UserManager<NorthPoleUser> userManager)
        {

            var user = new NorthPoleUser()
            {
                UserName = "Elio",
                Email = "fezollarielio@gmail.com",
                FirstName = "Elio",
                LastName = "Test",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Address = "Random Address",
                City = "Korce",
                Country = "Albania"
            };
            await userManager.CreateAsync(user, "Passw@rd!23");
            await userManager.AddToRoleAsync(user, Enum.Roles.Traveler.ToString());

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
                            CarId = 1 ,
                            UserId = user.Id,
                        }, 
                        new CarBooking()
                        {
                            BookedStartDate = DateTime.Now.AddDays(4),
                            BookedEndDate= DateTime.Now.AddDays(5),
                            CarId = 1,
                            UserId = user.Id,
                        },
                        new CarBooking()
                        {
                            BookedStartDate = DateTime.Now.AddDays(5),
                            BookedEndDate= DateTime.Now.AddDays(7),
                            CarId = 2,
                            UserId = user.Id,
                        },
                        new CarBooking()
                        {
                            BookedStartDate = DateTime.Now.AddDays(4),
                            BookedEndDate= DateTime.Now.AddDays(6),
                            CarId = 2,
                            UserId = user.Id,
                        },
                        new CarBooking
                        {
                            BookedStartDate = DateTime.Now.AddDays(3),
                            BookedEndDate = DateTime.Now.AddDays(8),
                            CarId = 3,
                            UserId = user.Id,
                        },
                        new CarBooking
                        {   
                        BookedStartDate = DateTime.Now.AddDays(6),
                        BookedEndDate = DateTime.Now.AddDays(9),
                        CarId = 4,
                        UserId = user.Id,
                        },
                         new CarBooking
                         {
                        BookedStartDate = DateTime.Now.AddDays(2),
                        BookedEndDate = DateTime.Now.AddDays(7),
                        CarId = 5,
                        UserId = user.Id,
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
                if (!context.FlightBookings.Any())
                {
                    context.FlightBookings.AddRange(new List<FlightBooking>()
                    {
                        new FlightBooking
                        {
                            FlightId = 1,
                            PassengerName = "Adam Keyes",
                            PassportNumber = "7577H123BHF",
                            NumberOfPassenger = 2,
                            UserId = user.Id,
                        },
                        new FlightBooking
                        {
                            FlightId = 2,
                            PassengerName = "Emily Johnson",
                            PassportNumber = "ABC456DEF",
                            NumberOfPassenger = 1,
                            UserId = user.Id,
                        },
                        new FlightBooking
                        {
                            FlightId = 3,
                            PassengerName = "Michael Davis",
                            PassportNumber = null, // No passport for this booking
                            NumberOfPassenger = 3,
                            UserId = user.Id,
                        },
                        new FlightBooking
                        {
                            FlightId = 4,
                            PassengerName = "Sophia Smith",
                            PassportNumber = "XYZ789UVW",
                            NumberOfPassenger = 1,
                            UserId = user.Id,
                        },
                        new FlightBooking
                        {
                            FlightId = 5,
                            PassengerName = "Daniel White",
                            PassportNumber = "JKL321MNO",
                            NumberOfPassenger = 4,
                            UserId = user.Id,
                        },
                        new FlightBooking
                        {
                            FlightId = 3,
                            PassengerName = "Olivia Brown",
                            PassportNumber = null, // No passport for this booking
                            NumberOfPassenger = 2,
                            UserId = user.Id,
                        }
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
                if (!context.Hotels.Any())
                {
                    context.Hotels.AddRange(new List<Hotel>()
                    {
                        new Hotel
                        {
                            HotelName = "Four Seasons Hotel",
                            City = "Toronto",
                            HotelLocation = "5 King West",
                            Description = "5 Star Hotel with full-accommodation"
                        },
                        new Hotel
                        {
                            HotelName = "Grand Plaza",
                            City = "New York",
                            HotelLocation = "123 Broadway",
                            Description = "Luxurious hotel in the heart of New York City"
                        },
                        new Hotel
                        {
                            HotelName = "Sunset Resort",
                            City = "Los Angeles",
                            HotelLocation = "789 Ocean Drive",
                            Description = "Beachfront resort with stunning sunset views"
                        },
                        new Hotel
                        {
                            HotelName = "Mountain View Lodge",
                            City = "Vancouver",
                            HotelLocation = "456 Mountain Road",
                            Description = "Cozy lodge nestled in the mountains"
                        },
                        new Hotel
                        {
                            HotelName = "City Lights Inn",
                            City = "San Francisco",
                            HotelLocation = "10 Downtown Street",
                            Description = "Centrally located hotel with beautiful city views"
                        },
                        new Hotel
                        {
                            HotelName = "Seaside Retreat",
                            City = "Miami",
                            HotelLocation = "777 Beach Boulevard",
                            Description = "Relaxing seaside retreat with private beach access"
                        }

                    });
                    context.SaveChanges();
                }
                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(new List<Room>()
                    {
                        new Room
                        {
                            Description = "Twin Deluxe Room",
                            Price = 150.09,
                            MaxGuest = 5,
                            HotelId = 1
                        },
                        new Room
                        {
                            Description = "Executive Suite",
                            Price = 300.50,
                            MaxGuest = 3,
                            HotelId = 1
                        },
                        new Room
                        {
                            Description = "Standard Queen Room",
                            Price = 120.75,
                            MaxGuest = 2,
                            HotelId = 1
                        },

                        // Rooms for Grand Plaza (HotelId = 2)
                        new Room
                        {
                            Description = "Luxury King Suite",
                            Price = 250.99,
                            MaxGuest = 4,
                            HotelId = 2
                        },
                        new Room
                        {
                            Description = "Double Queen Room",
                            Price = 180.25,
                            MaxGuest = 4,
                            HotelId = 2
                        },
                        new Room
                        {
                            Description = "Single Room",
                            Price = 100.50,
                            MaxGuest = 1,
                            HotelId = 2
                        },

                        // Rooms for Sunset Resort (HotelId = 3)
                        new Room
                        {
                            Description = "Ocean View Suite",
                            Price = 280.75,
                            MaxGuest = 3,
                            HotelId = 3
                        },
                        new Room
                        {
                            Description = "Beachfront Villa",
                            Price = 350.00,
                            MaxGuest = 6,
                            HotelId = 3
                        },
                        new Room
                        {
                            Description = "Cozy Bungalow",
                            Price = 200.99,
                            MaxGuest = 2,
                            HotelId = 3
                        },
                        new Room
                        {
                            Description = "Grand Suite",
                            Price = 400.99,
                            MaxGuest = 4,
                            HotelId = 4
                        },
                        new Room
                        {
                            Description = "Family Room",
                            Price = 220.50,
                            MaxGuest = 6,
                            HotelId = 4
                        },
                        new Room
                        {
                            Description = "Standard Double Room",
                            Price = 150.75,
                            MaxGuest = 2,
                            HotelId = 4
                        },

                        // Rooms for Mountain View Lodge (HotelId = 5)
                        new Room
                        {
                            Description = "Mountain View Cabin",
                            Price = 180.99,
                            MaxGuest = 3,
                            HotelId = 5
                        },
                        new Room
                        {
                            Description = "Woodland Retreat",
                            Price = 120.25,
                            MaxGuest = 2,
                            HotelId = 5
                        },
                        new Room
                        {
                            Description = "Rustic Chalet",
                            Price = 250.50,
                            MaxGuest = 4,
                            HotelId = 5
                        },

                        // Rooms for City Lights Inn (HotelId = 6)
                        new Room
                        {
                            Description = "City View Suite",
                            Price = 200.75,
                            MaxGuest = 3,
                            HotelId = 6
                        },
                        new Room
                        {
                            Description = "Urban Loft",
                            Price = 300.00,
                            MaxGuest = 2,
                            HotelId = 6
                        },
                        new Room
                        {
                            Description = "Downtown Studio",
                            Price = 180.99,
                            MaxGuest = 1,
                            HotelId = 6
                        }


                });
                    context.SaveChanges();
                }
                if (!context.RoomBookings.Any())
                {
                    context.RoomBookings.AddRange(new List<RoomBooking>()
                    {
                        new RoomBooking
                        {
                            BookedStartDate = DateTime.Now.AddDays(5),
                            BookedEndDate = DateTime.Now.AddDays(7),
                            RoomId = 1,
                            UserId = user.Id,
                        },
                        new RoomBooking
                        {
                            BookedStartDate = DateTime.Now.AddDays(10),
                            BookedEndDate = DateTime.Now.AddDays(12),
                            RoomId = 2,
                            UserId = user.Id,
                        },
                        new RoomBooking
                        {
                            BookedStartDate = DateTime.Now.AddDays(15),
                            BookedEndDate = DateTime.Now.AddDays(17),
                            RoomId = 3,
                            UserId = user.Id,
                        },
                        new RoomBooking
                        {
                            BookedStartDate = DateTime.Now.AddDays(20),
                            BookedEndDate = DateTime.Now.AddDays(22),
                            RoomId = 4,
                            UserId = user.Id,
                        },
                        new RoomBooking
                        {
                            BookedStartDate = DateTime.Now.AddDays(25),
                            BookedEndDate = DateTime.Now.AddDays(27),
                            RoomId = 5,
                            UserId = user.Id,
                        },
                        new RoomBooking
                        {
                            BookedStartDate = DateTime.Now.AddDays(30),
                            BookedEndDate = DateTime.Now.AddDays(32),
                            RoomId = 6,
                            UserId = user.Id,
                        },
                        new RoomBooking
                        {
                            BookedStartDate = DateTime.Now.AddDays(35),
                            BookedEndDate = DateTime.Now.AddDays(37),
                            RoomId = 7,
                            UserId = user.Id,
                        }


                    });
                        context.SaveChanges();
                }

            }
        }
    }
}
