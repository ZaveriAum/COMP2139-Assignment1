﻿using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        [Required]
        public string Airline { get; set;}

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string From {  get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public int EconomySeats { get; set; }

        [Required]
        public int PremiumSeats { get; set; }

        [Required]
        public string BusinessClassSeats { get; set; }

        [Required]
        public string FirstClass {  get; set; }
    }
}
