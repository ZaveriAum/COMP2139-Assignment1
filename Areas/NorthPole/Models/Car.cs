using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2139_Assignment1.Areas.NorthPole.Models
{
    public class Car
    {
        // Unique Id for car
        [Key]
        public int CarId { get; set; }

        // Car number for details of the car
        [Required]
        [Display(Name = "Plate Number")]
        [StringLength(15, ErrorMessage = "Plate number should not exceed the limit of 15 characters.")]
        public string PlateNumber { get; set; }

        [Required]
        [Display(Name = "Pickup Address")]
        [StringLength(100, ErrorMessage = "Pickup address should not exceed the limit of 100 characters.")]
        public string PickUpLocation { get; set; }

        // The city from where the car is in this is used in search.
        [Required]
        [Display(Name = "City")]
        [StringLength(30, ErrorMessage = "City name should not exceed the limit of 30 characters.")]
        public string City { get; set; }


        [Required]
        [StringLength(20, ErrorMessage = "Brand of the car should not exceed the limit of 20 characters.")]
        [Display(Name = "Car  Brand")]
        public string Brand { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Model of the car should not exceed the limit of 20 characters.")]
        [Display(Name = "Car Model")]
        public string Model { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Description should not exceed the limit of 100 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Price Per Day")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Rental Company")]
        [StringLength(50, ErrorMessage = "Company name cannot exceed the limit of 50 characters.")]
        public string RentalCompany { get; set; }

        [Required]
        [Display(Name = "Max Passenger")]
        public int MaxPassenger { get; set; }

        [AllowNull]
        [Display(Name = "Rating")]
        [Range(0, 5)]
        public double? Rating { get; set; }

        public List<CarBooking>? CarBookings { get; set; }

    }
}
