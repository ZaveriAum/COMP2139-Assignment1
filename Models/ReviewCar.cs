using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class ReviewCar
    {

        [Key]
        public int ReviewCarId { get; set; }

        [Required]
        [Display(Name = "Review")]
        [StringLength(200, ErrorMessage = "Review should not exceed the limit of 200 characters.")]
        public string Review { get; set; }

        // Forign key form the Car table
        [Required]
        public int CarId { get; set; }
    }
}
