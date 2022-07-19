using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public string? City { get; set; }
        public int DistanceToCenter { get; set; }
        public int BedsNumber { get; set; }
        public string? ApartmentDescription { get; set; }
        public string? ApartmentImage { get; set; }
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [DataType(DataType.Date)]
        public string? To { get; set; }
        public string? HostUserId { get; set; }
        public string? GuestUserId { get; set; }
        public bool IsPending { get; set; } = true;
        public bool IsAccepted { get; set; } = false;
    }
}
