using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Hotel
    {
        [Key]
        public string HotelId { get; set; }

        [Required]
        public string HotelName { get; set; }

        [Required]
        public string HotelLocation { get; set; }

        [Required]
        public string Features { get; set; }

        [Required]
        public int? Rating { get; set; }

        [Required]
        public string Description { get; set; }
    }
}