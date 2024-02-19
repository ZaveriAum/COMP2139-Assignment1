using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
	public class Flight
	{
		[Key]
		public int FlightId { get; set; }

		[Required]
		[RegularExpression(@"^[A-Z]{2}\d{3,4}$", ErrorMessage = "Flight number must start with two uppercase letters followed by three or four digits.")]
		public string FlightNumber { get; set; }

		[Required]
		public string Airline { get; set; }

        [Required]
        public DateOnly DepartureDate { get; set; }

        [Required]
		public DateOnly ArrivalDate { get; set; }

		[Required]
		public TimeOnly DepartureTime { get; set; }

		[Required]
		public TimeOnly ArrivalTime { get; set; }

		[Required]
		public int Price { get; set; }

		[Required]
		public string From { get; set; }

		[Required]
		public string To { get; set; }

		[Required]
		public int Seats { get; set; }
	}
}
