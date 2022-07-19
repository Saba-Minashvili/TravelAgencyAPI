using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class BookRequestDto
    {
        [JsonProperty("guestUserId")]
        public string? GuestUserId { get; set; }
        [JsonProperty("hostUserId")]
        public string? HostUserId { get; set; }
        [JsonProperty("from")]
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [JsonProperty("to")]
        [DataType(DataType.Date)]
        public string? To { get; set; }
    }
}
