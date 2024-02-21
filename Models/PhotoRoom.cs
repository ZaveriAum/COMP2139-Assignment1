using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class PhotoRoom
    {
        [Key] 
        public int PhotoRoomId { get; set; }

        [Required]
        public string PhotoPath { get; set; }

        // Forign Key from Room
        [Required]
        public int RoomId { get; set; }
    }
}
