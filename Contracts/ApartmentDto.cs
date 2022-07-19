using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class ApartmentDto
    {
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("address")]
        public string? Address { get; set; }
        [JsonProperty("distanceToCenter")]
        public int DistanceToCenter { get; set; }
        [JsonProperty("bedsNumber")]
        public int BedsNumber { get; set; }
        [JsonProperty("imageBase64")]
        public string? ImageBase64 { get; set; }
        [JsonProperty("from")]
        [DataType(DataType.Date)]
        public string? From { get; set; }
        [JsonProperty("to")]
        [DataType(DataType.Date)]
        public string? To { get; set; }
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
        [JsonProperty("hostUserId")]
        public string? HostUserId { get; set; }
    }
}
