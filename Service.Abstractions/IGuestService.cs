using Contracts;

namespace Services.Abstractions
{
    public interface IGuestService
    {
        Task<IEnumerable<GuestDto>> MyGuestsAsync(string hostUserId, CancellationToken cancellationToken = default);
        Task<GuestDto> GetGuestByIdAsync(string? guestUserId, CancellationToken cancellationToken = default);
        Task<GuestDto> AddGuestAsync(GuestDto guestDto, CancellationToken cancellationToken = default);
    }
}
