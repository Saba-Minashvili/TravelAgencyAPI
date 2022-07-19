using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class SearchApartmentDto
    {
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("from")]
        public string? From { get; set; }
        [JsonProperty("to")]
        public string? To { get; set; }
    }
}
