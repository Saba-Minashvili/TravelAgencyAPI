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

            if (apartments == null)
            {
                return BadRequest("Unable to get data of apartments");
            }

            return Ok(apartments);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetApartmentByUserId(string? userId, CancellationToken cancellationToken = default)
        {
            if(userId == null || userId == String.Empty)
            {
                return BadRequest($"Invalid request. {nameof(userId)} cannnot be null or empty.");
            }

            var apartment = await _serviceManager.ApartmentService.GetByUserIdAsync(userId, cancellationToken);

            if(apartment == null)
            {
                return BadRequest("Unable to get apartment.");
            }

            return Ok(apartment);
        }

        [HttpGet("[action]/{bedsNumber}")]
        public async Task<IActionResult> FilterByBedsNumber(int bedsNumber, CancellationToken cancellationToken = default)
        {
            if(bedsNumber == 0)
            {
                return BadRequest($"Invalid request. {nameof(bedsNumber)} cannot be 0.");
            }
            var apartments = await _serviceManager.ApartmentService.FilterByBedsNumber(bedsNumber, cancellationToken);

            if(apartments == null)
            {
                return BadRequest("Unable to get apartments");
            }

            return Ok(apartments);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SearchApartment([FromBody] SearchApartmentDto searchApartmentDto, CancellationToken cancellationToken = default)
        {
            var apartments = await _serviceManager.ApartmentService.SearchApartmentAsync(searchApartmentDto, cancellationToken);

            if(apartments == null)
            {
                return BadRequest("Unable to get data of apartments");
            }

            return Ok(apartments);
        }

        [HttpPost]
        public async Task<IActionResult> AddApartment([FromBody] ApartmentDto apartmentDto, CancellationToken cancellationToken = default)
        {
            var apartment = await _serviceManager.ApartmentService.CreateAsync(apartmentDto, cancellationToken);

            if(apartment == null)
            {
                return BadRequest("Unable to add apartment.");
            }

            return Ok(apartment);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateApartment(string? userId, [FromBody] ApartmentDto apartmentDto, CancellationToken cancellationToken = default)
        {
            if (userId == null || userId == String.Empty)
            {
                return BadRequest($"Invalid request. {nameof(userId)} cannnot be null or empty.");
            }

            await _serviceManager.ApartmentService.UpdateAsync(userId, apartmentDto, cancellationToken);

            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteApartment(string? userId, CancellationToken cancellationToken = default)
        {
            if (userId == null || userId == String.Empty)
            {
                return BadRequest($"Invalid request. {nameof(userId)} cannnot be null or empty.");
            }

            await _serviceManager.ApartmentService.DeleteAsync(userId, cancellationToken);

            return Ok();
        }
    }
}
