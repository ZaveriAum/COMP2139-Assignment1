using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Highlights { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public DateTime AvailabilityStartDate { get; set; }

        [Required]
        public DateTime AvailabilityEndDate { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        // foreign key for Hotel
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

    }
}
