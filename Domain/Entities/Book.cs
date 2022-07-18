using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        public string? City { get; set; }
        [Required]
        public int DistanceToCenter { get; set; }
        [Required]
        public int BedsNumber { get; set; }
        [Required]
        public string? ApartmentDescription { get; set; }
        [Required]
        public string? ApartmentImage { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string? To { get; set; }
        public string? HostUserId { get; set; }
        public string? GuestUserId { get; set; }
        public bool IsPending { get; set; } = true;
        public bool IsAccepted { get; set; } = false;
    }
}
