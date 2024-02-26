using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace COMP2139_Assignment1.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [Required]
        public string HotelName { get; set;}

        [Required]
        [Display(Name = "City")]
        public string City { get; set;}

        [Required]
        public string HotelLocation { get; set;}

        [Required]
        public string Description { get; set;}


    }
}
