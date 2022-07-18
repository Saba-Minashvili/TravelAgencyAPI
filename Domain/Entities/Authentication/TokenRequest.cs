using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Authentication
{
    public class TokenRequest
    {
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
