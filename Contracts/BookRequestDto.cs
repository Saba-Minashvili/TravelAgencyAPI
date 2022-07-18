using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class BookRequestDto
    {
        [Required]
        [JsonProperty("guestUserId")]
        public string? GuestUserId { get; set; }
        [Required]
        [JsonProperty("hostUserId")]
        public string? HostUserId { get; set; }
        [Required]
        [JsonProperty("from")]
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [Required]
        [JsonProperty("to")]
        [DataType(DataType.Date)]
        public string? To { get; set; }
    }
}
