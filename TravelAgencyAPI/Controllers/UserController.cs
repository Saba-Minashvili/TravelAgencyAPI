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
            var result = await _serviceManager.UserService.GetAllAsync(cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetById(string userId, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.UserService.GetByIdAsync(userId, cancellationToken);

            return Ok(result);
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userDto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.UserService.CreateAsync(userDto, cancellationToken);

            return Ok();
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto userDto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.UserService.UpdateAsync(userId, userDto, cancellationToken);

            return Ok(result);
        }
    }
}