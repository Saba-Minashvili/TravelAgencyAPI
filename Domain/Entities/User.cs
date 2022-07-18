using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Login { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public Apartment? Apartment { get; set; }
        public List<Guest>? MyGuests { get; set; }
        public List<Book>? MyBookings { get; set; }
    }
}
