using Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace TravelAgency.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BookRequestController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BookRequestController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpPost("SendBookRequest")]
        public async Task SendBookRequestAsync([FromBody] BookRequestDto bookRequestDto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.BookRequestService.SendBookRequestAsync(bookRequestDto, cancellationToken);
        }
    }
}
