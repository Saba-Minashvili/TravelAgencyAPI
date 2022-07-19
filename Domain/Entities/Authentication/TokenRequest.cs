using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Authentication
{
    public class TokenRequest
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
