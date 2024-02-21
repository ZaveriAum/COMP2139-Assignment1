using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class PhotoHotel
    {
        [Key]   
        public int PhotoHotelId { get; set; }

        [Required]
        public string PhotoPath { get; set;}

        // Forign key form Hotel
        [Required]
        public int HotelId { get; set; }
    }
}
