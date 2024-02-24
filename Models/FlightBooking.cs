using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class FlightBooking
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name should not exceed 50 characters")]
        public string PassengerName { get; set; }

        // Passport information (for international flights)
        public string? PassportNumber { get; set; }
    }

}
