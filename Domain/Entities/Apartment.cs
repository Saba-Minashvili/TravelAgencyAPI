using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Apartment:BaseEntity
    {
        public string? City { get; set; }
        public string? Address { get; set; }
        public int DistanceToCenter { get; set; }
        public int BedsNumber { get; set; }
        public string? ImageBase64 { get; set; }
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [DataType(DataType.Date)]
        public string? To { get; set; }
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
