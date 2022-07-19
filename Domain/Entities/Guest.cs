using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Guest:BaseEntity
    {
        public string? UserPhoto { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [DataType(DataType.Date)]
        public string? To { get; set; }
        public string? Description { get; set; }
        public string? HostUserId { get; set; }
        public string? GuestUserId { get; set; }
    }
}
