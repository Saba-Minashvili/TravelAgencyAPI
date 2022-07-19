using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts
{
    public class UpdateUserDto
    {
        [NotMapped]
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [NotMapped]
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        public string? Login
        {
            get { return FirstName + LastName; }
        }
        [JsonProperty("photo")]
        public string? Photo { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}
