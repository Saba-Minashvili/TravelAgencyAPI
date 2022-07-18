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
        public async Task AcceptGuestRequest(string? guestUserId, string? hostUserId, CancellationToken cancellationToken = default)
        {
            await _serviceManager.GuestRequestService.AcceptGuestRequestAsync(guestUserId, hostUserId, cancellationToken);
        }

        [HttpPut("[action]/{guestUserId}/{hostUserId}")]
        public async Task DeclineGuestRequest(string? guestUserId, string? hostUserId, CancellationToken cancellationToken = default)
        {
            await _serviceManager.GuestRequestService.DeclineGuestRequestAsync(guestUserId, hostUserId, cancellationToken);
        }
    }
}
