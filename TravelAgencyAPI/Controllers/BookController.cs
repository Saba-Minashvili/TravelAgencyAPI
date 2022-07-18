using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace TravelAgency.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BookController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet("MyBookings/{userId}")]
        public async Task<IActionResult> GetMyBookings(string userId, CancellationToken cancellationToken = default)
        {
            var myBookings = await _serviceManager.BookService.GetMyBookingsAsync(userId, cancellationToken);

            return Ok(myBookings);
        }

    }
}
