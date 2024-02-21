using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [Display(Name ="Description")]
        [StringLength(300, ErrorMessage ="Room description should not exceed the limit of 300 character.")]
        public string Description { get; set; }

        [Required]
        [Display(Name ="Price")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        [Display(Name ="Rating")]
        [Range(0,5)]
        public int Rating { get; set; }

        // Foreign key from the Hotel where hotel can have muliple room but room can only belong to one hotel.
        [Required]
        public int HotelId { get; set; }

        // For navigation
        [Required]
        public Hotel? hotel { get; set; }
    }
}
