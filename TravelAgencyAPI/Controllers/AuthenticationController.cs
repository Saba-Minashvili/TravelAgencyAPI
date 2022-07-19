using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace TravelAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IJwtAuthenticationService _jwtAuth;

        public AuthenticationController(IJwtAuthenticationService jwtAuth)
        {
            _jwtAuth = jwtAuth;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<TokenResponse?> AuthenticateAsync([FromBody] TokenRequest request)
        {
            return await _jwtAuth.Authenticate(request);
        }
    }
}
