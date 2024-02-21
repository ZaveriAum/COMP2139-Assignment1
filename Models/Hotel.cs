using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Hotel
    {
        //Unique Id
        [Key]
        public int HotelId { get; set; }

        [Required]
        [Display(Name ="Hote Name")]
        [StringLength(50, ErrorMessage ="Hotel Name should not exceed the limit of 50 characters.")]
        public string HotelName { get; set; }

        [Required]
        [Display(Name ="Hotel Description")]
        [StringLength(200, ErrorMessage ="Hotel Description shiuld not exceed the limit of 200 characters.")]
        public string HotelDescription { get; set; }

        [Required]
        [Display(Name ="Hotel City")]
        [StringLength(50, ErrorMessage ="Hotel city should not exceed the limit of 50 characters.")]
        public string HotelCity { get; set; }

        [Required]
        [Display(Name ="Hotel Location")]
        [StringLength(300, ErrorMessage ="Hotel location should not exceed the limit of 300 characters.")]
        public string HotelLocation { get; set; }

        [Required]
        [Display(Name ="Rating")]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
