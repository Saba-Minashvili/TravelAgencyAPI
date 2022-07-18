using Domain.Entities;
using Domain.Entities.Authentication;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistence.Authentication
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly Token _token;

        public JwtAuthenticationService(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IOptions<Token> tokenOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _token = tokenOptions.Value;
        }

        public async Task<TokenResponse?> Authenticate(TokenRequest request)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            if (await IsValidUser(request.Login, request.Password))
            {
                User user = await GetUserByName(request.Login);

                if (user != null)
                {
                    var result = GenerateJwtToken(user);

                    await _userManager.UpdateAsync(user);

                    return new TokenResponse() { Token = result.Item1, ExpireAt = result.Item2, UserId = user.Id };
                }
            }
            else
            {
                throw new InvalidCredentialsException();
            }

            return null;
        }

        private async Task<bool> IsValidUser(string login, string password)
        {
            User user = await GetUserByName(login);

            if (user == null)
            {
                // Username or password was incorrect
                return false;
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);

            return signInResult.Succeeded;
        }

        private async Task<User> GetUserByName(string login)
        {
            return await _userManager.FindByNameAsync(login);
        }

        private Tuple<string, DateTime?> GenerateJwtToken(User user)
        {
            byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.NameIdentifier, user.Login)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = handler.CreateToken(descriptor);
            string jwtToken = handler.WriteToken(securityToken);

            return new Tuple<string, DateTime?>(jwtToken, descriptor.Expires);
        }
    }
}
