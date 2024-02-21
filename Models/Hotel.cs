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
        public string HotelLocation { get; set;}

        [Required]
<<<<<<< Updated upstream
        public string Features { get; set;}

=======
>>>>>>> Stashed changes
        public int? Rating { get; set;}

        [Required]
        public string Description { get; set;}

        [Required]
        public int PricePerNight { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public DateTime AvailabilityStartDate { get; set; }

        [Required]
        public DateTime AvailabilityEndDate { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public List<Room>? Rooms { get; set; }

        public Hotel()
        {
            // Set default values
            IsAvailable = true;
        }


    }
}
