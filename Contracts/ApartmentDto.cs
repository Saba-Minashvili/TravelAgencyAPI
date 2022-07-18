using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class ApartmentDto
    {
        [Required]
        [JsonProperty("city")]
        public string? City { get; set; }
        [Required]
        [JsonProperty("address")]
        public string? Address { get; set; }
        [Required]
        [JsonProperty("distanceToCenter")]
        [Range(0,10, ErrorMessage = "Distance to center must be from 0-10km range.")]
        public int DistanceToCenter { get; set; }
        [Required]
        [JsonProperty("bedsNumber")]
        [Range(1,10, ErrorMessage = "Beds number must be from 1-10 range.")]
        public int BedsNumber { get; set; }
        [Required]
        [JsonProperty("imageBase64")]
        public string? ImageBase64 { get; set; }
        [Required]
        [JsonProperty("from")]
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [Required]
        [JsonProperty("to")]
        [DataType(DataType.Date)]
        public string? To { get; set; }
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Description cannot exceed 150 characters.")]
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("isTaken")]
        public bool? IsTaken { get; set; }
        [JsonProperty("isTakenFrom")]
        [DataType(DataType.Date)]
        public string? IsTakenFrom { get; set; }
        [JsonProperty("isTaken")]
        [DataType(DataType.Date)]
        public string? IsTakenTo { get; set; }
        [Required]
        [JsonProperty("hostUserId")]
        public string? HostUserId { get; set; }
    }
}
