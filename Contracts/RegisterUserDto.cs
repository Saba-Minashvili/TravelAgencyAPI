using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts
{
    public class RegisterUserDto
    {
        [Required]
        [NotMapped]
        [StringLength(maximumLength: 10, ErrorMessage = "Name cannot exceed 10 characters.")]
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [Required]
        [NotMapped]
        [StringLength(maximumLength: 10, ErrorMessage = "Name cannot exceed 10 characters.")]
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Login cannot exceed 20 characters.")]
        public string? Login
        {
            get { return FirstName + LastName; }
        }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 12, ErrorMessage = "Password can include minimum 7, maximum 12 characters", MinimumLength = 7)]
        [JsonProperty("password")]
        public string? Password { get; set; }
        [JsonProperty("photo")]
        public string? Photo { get; set; }
        [Required]
        [EmailAddress]
        [JsonProperty("email")]
        public string? Email { get; set; }
        [StringLength(maximumLength: 150, ErrorMessage = "Description cannot exceed 150 characters.")]
        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}
