using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class UserAuthDto
    {
        [Required]
        [JsonProperty("login")]
        public string? Login { get; set; }
        [Required]
        [JsonProperty("password")]
        public string? Password { get; set; }
    }
}
