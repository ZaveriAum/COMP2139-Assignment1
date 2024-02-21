using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Car
    {
        // Unique Id for car
        [Key]
        public int CarId { get; set; }

        // Car number for details of the car
        [Required]
        [Display(Name ="Plate Number")]
        [StringLength(15, ErrorMessage ="Plate number should not exceed the limit of 15 characters.")]
        public string PlateNumber { get; set; }

        // The city from where the car is in this is used in search.
        [Required]
        [Display(Name ="City")]
        [StringLength(30, ErrorMessage ="CIty name should not exceed the limit of 30 characters.")]
        public string City { get; set; }

        // Pickup location of the card based on the city for the details page
        [Required]
        [Display(Name ="Pickup Address")]
        [StringLength(100, ErrorMessage ="Pickup address should not exceed the limit of 100 characters.")]
        public string PickUpLocation { get; set; }

        [Required]
        [StringLength(20, ErrorMessage ="Make of the car should not exceed the limit of 20 characters.")]
        [Display(Name ="Car Make")]
        public string Make { get; set; }

        [Required]
        [StringLength(20, ErrorMessage ="Model of the car shoiuld not exceed the limit of 20 characters.")]
        [Display(Name ="Car Model")]
        public string Model { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="Description should not exceed the limit of 100 characters.")]
        [Display(Name ="Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name ="Price")]
        public int Price { get; set; }

        [Required]
        [Display(Name ="Rental Company")]
        [StringLength(50, ErrorMessage ="Company name cannot exceed the limit of 50 characters.")]
        public string RentalCompany {  get; set; }

        [Required]
        [Display(Name ="Rating")]
        [Range(0, 5)]
        public int Rating { get; set; }

       
    }
}
