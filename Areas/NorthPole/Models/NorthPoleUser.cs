using Microsoft.AspNetCore.Identity;

namespace COMP2139_Assignment1.Areas.NorthPole.Models
{
    public class NorthPoleUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}
