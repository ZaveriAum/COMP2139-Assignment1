using Microsoft.AspNetCore.Identity;

namespace COMP2139_Assignment1.Areas.NorthPole.Models
{
    public class NorthPoleUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
    }
}
