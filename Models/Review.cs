﻿using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Review
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        public string Comment {  get; set; }

        public string CarPlateNumber { get; set; }

        public Car? Car {  get; set; }
        
        public string? RoomId { get; set; }

        public Room? Room { get; set; }

        public string? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}