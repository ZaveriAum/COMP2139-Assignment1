using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Areas.NorthPole.Models
{
    public class RoomBooking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime BookedStartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime BookedEndDate { get; set; }

        [Required]
        public int RoomId { get; set; }

        public Room? Room { get; set; }
        [Required]
        public string UserId { get; set; }
        public NorthPoleUser? User { get; set; }
    }
}
