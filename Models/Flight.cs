using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
	public class Flight
	{
		// Unique Flight ID
		[Key]
		public int FlightId { get; set; }

		// Flight Number of the flight.
		[Required]
		[Display(Name ="Flight Number")]
		[RegularExpression(@"^[A-Z]{2}\d{3,4}$", ErrorMessage = "Flight number must start with two uppercase letters followed by three or four digits.")]
		public string FlightNumber { get; set; }

		// The Airline from whcih the flight is getting offered
		[Required]
		[Display(Name ="Airline")]
		[StringLength(50, ErrorMessage ="Airline name lenght should not exceed the limit of 50 characters")]
		public string Airline { get; set; }
		// What is the Date of take off on the home location.
        [Required]
		[Display(Name ="Departure Date")]
		[DataType(DataType.Date)]
        public DateOnly DepartureDate { get; set; }

		// What is the date of arrival on the destination this is set by the arline therefore it will depend of the timezone of the destination
        [Required]
		[Display(Name ="Arrival Date")]
		[DataType(DataType.Date)]
		public DateOnly ArrivalDate { get; set; }

		// At what time will the flight take off on the give date
		[Required]
		[Display(Name ="Departure Time")]
		[DataType(DataType.Time)]
		public TimeOnly DepartureTime { get; set; }

		// What time will it arrive at the decided Destination
		[Required]
		[Display(Name ="Arrival Time")]
		[DataType(DataType.Time)]
		public TimeOnly ArrivalTime { get; set; }

		// What is the price of the flight
		[Required]
		[DataType(DataType.Currency)]
		[Display(Name ="Price")]
		public int Price { get; set; }

		// From where is this flight taking off
		[Required]
		[Display(Name ="From")]
		public string From { get; set; }

		// What is the destination if the flight going to be
		[Required]
		[Display(Name ="Destinaiton")]
		public string To { get; set; }
		
		// Number of seats offered by the flight
		[Required]
		[Display(Name ="Seats")]
		public int Seats { get; set; }
	}
}
