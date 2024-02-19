using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Room
    {
        [Key]
        public string RoomId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Highlights { get; set; }

        [Required]
        public int Price { get; set; }

        public int? Rating { get; set; }
        // Foreign key for Hotel
        public int HotelId { get; set; }

        // Navigation property
        public Hotel? Hotel { get; set; }
    }
}
