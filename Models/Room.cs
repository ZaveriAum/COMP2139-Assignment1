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
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;


        // foreign key for Hotel
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }


    }
}
