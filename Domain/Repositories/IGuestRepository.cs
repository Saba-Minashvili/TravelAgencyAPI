using Domain.Entities;

namespace Domain.Repositories
{
    public interface IGuestRepository
    {
        Task<IEnumerable<Guest>> MyGuestsAsync(string? hostUserId, CancellationToken cancellationToken = default);
        Task<Guest?> GetGuestByIdAsync(string? guestUserId, CancellationToken cancellationToken = default);
        void AddGuestAsync(Guest guest);
    }
}
