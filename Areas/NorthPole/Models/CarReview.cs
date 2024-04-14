using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Areas.NorthPole.Models
{
    public class CarReview
    {
        [Key]
        public int Id { get; set; }
        [Range(0, 10, ErrorMessage = "Value must be between 0 and 10")]
        [Required]
        public int Rating { get; set; }
        [Range(0, 500, ErrorMessage = "Comment must be between 0 and 500")]
        public string? Comment {  get; set; }
        [Required]
        [Display(Name = "Date Posted")]
        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }
        [Required]

        public bool isAnonymous {  get; set; }
        [Required]
        public string UserId {  get; set; }
        public NorthPoleUser? User { get; set; }
        [Required]
        public int CarId { get; set; }
        public Car? Car { get; set; }

    }
}
