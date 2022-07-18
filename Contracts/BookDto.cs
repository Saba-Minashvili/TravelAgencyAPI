using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class BookDto
    {
        [Required]
        [JsonProperty("city")]
        public string? City { get; set; }
        [Required]
        [JsonProperty("distanceToCenter")]
        public int DistanceToCenter { get; set; }
        [Required]
        [JsonProperty("bedsNumber")]
        public int BedsNumber { get; set; }
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Description cannot exceed 150 characters.")]
        [JsonProperty("description")]
        public string? ApartmentDescription { get; set; }
        [Required]
        [JsonProperty("apartmentImage")]
        public string? ApartmentImage { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [JsonProperty("from")]
        public string? From { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [JsonProperty("to")]
        public string? To { get; set; }
        [Required]
        [JsonProperty("hostUserId")]
        public string? HostUserId { get; set; }
        [Required]
        [JsonProperty("guestUserId")]
        public string? GuestUserId { get; set; }
        public bool IsPending { get; set; } = true;
        public bool IsAccepted { get; set; } = false;
    }
}
