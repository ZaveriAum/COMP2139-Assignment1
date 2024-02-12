using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Reviews
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        public string Review {  get; set; }

        public string CarPlateNumber { get; set; }

        public Car? Car {  get; set; }
        
        public string? RoomId { get; set; }

        public Room? Room { get; set; }

        public string? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
