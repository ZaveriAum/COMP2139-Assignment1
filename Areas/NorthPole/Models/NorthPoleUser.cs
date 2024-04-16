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

        // ProfilePicture and Address can be optional, so no Data Annotations are necessary
        public byte[]? ProfilePicture { get; set; }
        public string? Address { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; }
    }
}
