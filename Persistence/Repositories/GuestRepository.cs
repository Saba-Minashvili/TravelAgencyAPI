using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class GuestRepository : IGuestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GuestRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Guest>> MyGuestsAsync(string? hostUserId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Guests == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            return await _dbContext.Guests
                .Where(o => o.HostUserId == hostUserId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Guest?> GetGuestByIdAsync(string? guestUserId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Guests == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            return await _dbContext.Guests
                .FirstOrDefaultAsync(o => o.GuestUserId == guestUserId, cancellationToken);
        }

        public void AddGuestAsync(Guest guest)
        {
            if (_dbContext.Guests == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            _dbContext.Guests.Add(guest);
        }
    }
}
