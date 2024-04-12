using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Areas.NorthPole.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Range(0, 10, ErrorMessage = "Value must be between 0 and 10")]
        [Required]
        public int Rating { get; set; }
        [Range(0, 500, ErrorMessage = "Comment must be between 0 and 500")]
        public string? Comment {  get; set; }
    }
}
