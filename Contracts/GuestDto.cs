using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class GuestDto
    {
        [JsonProperty("userPhoto")]
        public string? UserPhoto { get; set; }
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [DataType(DataType.Date)]
        [JsonProperty("from")]
        public string? From { get; set; }
        [DataType(DataType.Date)]
        [JsonProperty("to")]
        public string? To { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("hostUserId")]
        public string? HostUserId { get; set; }
        [JsonProperty("guestUserId")]
        public string? GuestUserId { get; set; }
    }
}
