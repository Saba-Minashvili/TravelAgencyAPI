using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class GuestDto
    {
        [Required]
        [JsonProperty("userPhoto")]
        public string? UserPhoto { get; set; }
        [Required]
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [Required]
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [JsonProperty("from")]
        public string? From { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [JsonProperty("to")]
        public string? To { get; set; }
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Description cannot exceed 150 characters.")]
        [JsonProperty("description")]
        public string? Description { get; set; }
        [Required]
        [JsonProperty("hostUserId")]
        public string? HostUserId { get; set; }
        [Required]
        [JsonProperty("guestUserId")]
        public string? GuestUserId { get; set; }
    }
}
