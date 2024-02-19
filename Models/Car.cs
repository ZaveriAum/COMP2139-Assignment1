using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Car
    {
        [Key]
        public string PlateNumber { get; set; }

        [Required]
        public string PickUpLocation { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string CarModel { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public string RentalCompany {  get; set; }

        public int Rating { get; set; }

       
    }
}
