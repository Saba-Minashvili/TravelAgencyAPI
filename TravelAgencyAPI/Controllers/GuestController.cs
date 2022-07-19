using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace TravelAgency.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public GuestController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet("MyGuests/{userId}")]
        public async Task<IActionResult> MyGuests(string? userId, CancellationToken cancellationToken = default)
        {
            if (userId == null || userId == String.Empty)
            {
                return BadRequest($"Invalid request. {nameof(userId)} cannnot be null or empty.");
            }

            var guests = await _serviceManager.GuestService.MyGuestsAsync(userId, cancellationToken);

            if(guests == null)
            {
                return BadRequest("Unable to get data of guests");
            }

            return Ok(guests);
        }
    }
}
