using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class SearchApartmentDto
    {
        [Required]
        [JsonProperty("city")]
        public string? City { get; set; }
        [Required]
        [JsonProperty("from")]
        public string? From { get; set; }
        [Required]
        [JsonProperty("to")]
        public string? To { get; set; }
    }
}
