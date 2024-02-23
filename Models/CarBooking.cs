using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class CarBooking
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
        public int CarId { get; set; }
        public Car? Car { get; set; }
    }
}
