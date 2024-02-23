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

        [Required]
        [Display(Name = "Seat Preference")]
        public string SeatPreference { get; set; } // You might want to use an enum for seat preferences

        // Additional services (e.g., extra baggage, in-flight meals)
        [Display(Name = "Extra Baggage")]
        public bool ExtraBaggage { get; set; }

        [Display(Name = "In-flight Meal")]
        public bool InFlightMeal { get; set; }
    }

}
