using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Guest:BaseEntity
    {
        [Required]
        public string? UserPhoto { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string? To { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? HostUserId { get; set; }
        public string? GuestUserId { get; set; }
    }
}
