using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Apartment:BaseEntity
    {
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public int DistanceToCenter { get; set; }
        [Required]
        public int BedsNumber { get; set; }
        [Required]
        public string? ImageBase64 { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string? To { get; set; }
        [Required]
        public string? Description { get; set; }
        public bool? IsTaken { get; set; }
        [DataType(DataType.Date)]
        public string? IsTakenFrom { get; set; }
        [DataType(DataType.Date)]
        public string? IsTakenTo { get; set; }
        public string? HostUserId { get; set; }
        public User? User { get; set; }
    }
}
