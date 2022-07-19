using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace TravelAgency.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UserController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var users = await _serviceManager.UserService.GetAllAsync(cancellationToken);

            if(users == null)
            {
                return BadRequest("Unable to get data of users");
            }

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string? userId, CancellationToken cancellationToken = default)
        {
            if (userId == null || userId == String.Empty)
            {
                return BadRequest($"Invalid request. {nameof(userId)} cannnot be null or empty.");
            }

            var user = await _serviceManager.UserService.GetByIdAsync(userId, cancellationToken);

            if(user == null)
            {
                return BadRequest("Unable to get user.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userDto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.UserService.CreateAsync(userDto, cancellationToken);

            return Ok();
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string? userId, [FromBody] UpdateUserDto userDto, CancellationToken cancellationToken = default)
        {
            if (userId == null || userId == String.Empty)
            {
                return BadRequest($"Invalid request. {nameof(userId)} cannnot be null or empty.");
            }

            var user = await _serviceManager.UserService.UpdateAsync(userId, userDto, cancellationToken);

            if(user == null)
            {
                return BadRequest("Invalid request. Unable to update user.");
            }

            return Ok(user);
        }
    }
}