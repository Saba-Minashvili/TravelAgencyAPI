using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace TravelAgency.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GuestRequestController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public GuestRequestController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpPut("[action]/{guestUserId}/{hostUserId}")]
        public async Task<IActionResult> AcceptGuestRequest(string? guestUserId, string? hostUserId, CancellationToken cancellationToken = default)
        {
            if (guestUserId != null && guestUserId != string.Empty)
            {
                if (hostUserId == null || hostUserId == string.Empty)
                {
                    return BadRequest($"Invalid request. UserId cannnot be null or empty.");
                }
            }

            await _serviceManager.GuestRequestService.AcceptGuestRequestAsync(guestUserId, hostUserId, cancellationToken);

            return Ok();
        }

        [HttpPut("[action]/{guestUserId}/{hostUserId}")]
        public async Task<IActionResult> DeclineGuestRequest(string? guestUserId, string? hostUserId, CancellationToken cancellationToken = default)
        {
            if (guestUserId != null && guestUserId != string.Empty)
            {
                if (hostUserId == null || hostUserId == string.Empty)
                {
                    return BadRequest($"Invalid request. UserId cannnot be null or empty.");
                }
            }

            await _serviceManager.GuestRequestService.DeclineGuestRequestAsync(guestUserId, hostUserId, cancellationToken);

            return Ok();
        }
    }
}
