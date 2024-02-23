﻿using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class RoomBooking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Start Date")]
        [DataType(DataType.Date)]
        public DateTime BookedStartDate { get; set; }

        [Required]
        [Display(Name ="End Date")]
        [DataType(DataType.Date)]
        public DateTime BookedEndDate { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public Room? Car { get; set; }
    }
}
