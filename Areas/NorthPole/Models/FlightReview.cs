using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Areas.NorthPole.Models
{
    public class FlightReview
    {
        [Key]
        public int Id { get; set; }
        [Range(0, 10, ErrorMessage = "Value must be between 0 and 10")]
        [Required]
        public int Rating { get; set; }
        [StringLength(500, ErrorMessage = "Comment must be at most 500 characters long.")]
        public string? Comment { get; set; }
        [Required]
        [Display(Name = "Date Posted")]
        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }
        [Required]

        public bool isAnonymous { get; set; }
        [Required]

        public string UserId { get; set; }
        public NorthPoleUser? User { get; set; }
        [Required]
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }
    }
}
