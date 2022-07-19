using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts
{
    public class RegisterUserDto
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
        [DataType(DataType.Password)]
        [JsonProperty("password")]
        public string? Password { get; set; }
        [JsonProperty("photo")]
        public string? Photo { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}
