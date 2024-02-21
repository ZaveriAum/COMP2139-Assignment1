using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class PhotoCar
    {
        [Key]
        public int PhotoCarId { get; set; }

        [Required]
        public string PhotoPath { get; set;}

        // Foreign key from Car
        [Required]
        public int CarId { get; set; }
    }
}
