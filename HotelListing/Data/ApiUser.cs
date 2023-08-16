using Microsoft.AspNetCore.Identity;

namespace HotelListing.Data
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
    }
}
