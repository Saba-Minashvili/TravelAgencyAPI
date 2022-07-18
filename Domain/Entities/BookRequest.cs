using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class BookRequest:BaseEntity
    {
        public string? GuestUserId { get; set; }
        public string? HostUserId { get; set; }
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [DataType(DataType.Date)]
        public string? To { get; set; }
    }
}