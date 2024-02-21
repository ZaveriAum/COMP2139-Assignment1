using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class ReviewHotel
    {
        [Key]
        public int ReviewHotelId { get; set; }

        [Required]
        [Display(Name ="Review")]
        [StringLength(200, ErrorMessage ="Review cannot exceed the limit of 200 characters.")]
        public string Review {  get; set; }

        // Foriegn key from Hotel table
        [Required]
        public int HotelId { get; set; }
    }
}
