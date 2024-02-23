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
                          Description= "Chevy Malibu - A stylish choice for your road trip.",
                          Price= 54.25,
                          RentalCompany= "Rocky Mountain Cars",
                          PickUpLocation= "321 Pine St"
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
