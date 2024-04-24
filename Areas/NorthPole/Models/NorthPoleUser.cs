using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Areas.NorthPole.Models
{
    public class NorthPoleUser : IdentityUser
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name must be between 1 and 50 characters.", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name must be between 1 and 50 characters.", MinimumLength = 1)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username change limit is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Username change limit must be a positive number.")]
        public int UsernameChangeLimit { get; set; }
        [StringLength(25, ErrorMessage = "Flyer number must be between 1 and 25 characters.", MinimumLength = 1)]
        public string? FrequentFlyerNumber { get; set; }
        [StringLength(25, ErrorMessage = "Hotel ID must be between 1 and 25 characters.", MinimumLength = 1)]

        public string? HotelLoyaltyNumber {  get; set; }

        // ProfilePicture and Address can be optional, so no Data Annotations are necessary
        public byte[]? ProfilePicture { get; set; }
        public string? Address { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }
    }
}
