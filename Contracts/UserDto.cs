namespace Contracts
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Login { get; set; }
        public string? Photo { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public ApartmentDto? Apartment { get; set; }
    }
}
