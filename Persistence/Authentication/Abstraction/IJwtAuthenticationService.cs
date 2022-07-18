using Domain.Entities.Authentication;

namespace Services.Abstractions
{
    public interface IJwtAuthenticationService
    {
        Task<TokenResponse?> Authenticate(TokenRequest request);
    }
}
