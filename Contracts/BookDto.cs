using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class BookDto
    {
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("distanceToCenter")]
        public int DistanceToCenter { get; set; }
        [JsonProperty("bedsNumber")]
        public int BedsNumber { get; set; }
        [StringLength(maximumLength: 150, ErrorMessage = "Description cannot exceed 150 characters.")]
        [JsonProperty("description")]
        public string? ApartmentDescription { get; set; }
        [JsonProperty("apartmentImage")]
        public string? ApartmentImage { get; set; }
        [DataType(DataType.Date)]
        [JsonProperty("from")]
        public string? From { get; set; }
        [DataType(DataType.Date)]
        [JsonProperty("to")]
        public string? To { get; set; }
        [JsonProperty("hostUserId")]
        public string? HostUserId { get; set; }
        [JsonProperty("guestUserId")]
        public string? GuestUserId { get; set; }
        public bool IsPending { get; set; } = true;
        public bool IsAccepted { get; set; } = false;
    }
}
