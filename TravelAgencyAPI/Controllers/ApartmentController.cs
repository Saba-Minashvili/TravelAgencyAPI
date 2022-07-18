using Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace TravelAgency.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ApartmentController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetApartments(CancellationToken cancellationToken = default)
        {
            var apartments = await _serviceManager.ApartmentService.GetAllAsync(cancellationToken);

            return Ok(apartments);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetApartmentByUserId(string userId, CancellationToken cancellationToken = default)
        {
            var apartment = await _serviceManager.ApartmentService.GetByUserIdAsync(userId, cancellationToken);

            return Ok(apartment);
        }

        [HttpGet("[action]/{bedsNumber}")]
        public async Task<IActionResult> FilterByBedsNumber(int bedsNumber, CancellationToken cancellationToken = default)
        {
            var apartments = await _serviceManager.ApartmentService.FilterByBedsNumber(bedsNumber, cancellationToken);

            return Ok(apartments);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SearchApartment([FromBody] SearchApartmentDto searchApartmentDto, CancellationToken cancellationToken = default)
        {
            var apartments = await _serviceManager.ApartmentService.SearchApartmentAsync(searchApartmentDto, cancellationToken);

            return Ok(apartments);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddApartment([FromBody] ApartmentDto apartmentDto, CancellationToken cancellationToken = default)
        {
            var apartment = await _serviceManager.ApartmentService.CreateAsync(apartmentDto, cancellationToken);

            return Ok(apartment);
        }

        [HttpPut("UpdateApartment/{userId}")]
        public async Task UpdateApartment(string userId, [FromBody] ApartmentDto apartmentDto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.ApartmentService.UpdateAsync(userId, apartmentDto, cancellationToken);
        }

        [HttpDelete("DeleteApartment/{userId}")]
        public async Task DeleteApartment(string userId, CancellationToken cancellationToken = default)
        {
            await _serviceManager.ApartmentService.DeleteAsync(userId, cancellationToken);
        }
    }
}
