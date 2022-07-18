using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    internal sealed class GuestRequestRepository : IGuestRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GuestRequestRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public void AcceptGuestRequestAsync(Book guestBooking, Guest guest)
        {
            if (_dbContext.Guests == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }else if(_dbContext.Bookings == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            _dbContext.Bookings.Update(guestBooking);
            _dbContext.Guests.Remove(guest);
        }

        public void DeclineGuestRequestAsync(Book guestBooking, Guest guest)
        {
            if (_dbContext.Guests == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }
            else if (_dbContext.Bookings == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            _dbContext.Bookings.Update(guestBooking);
            _dbContext.Guests.Remove(guest);
        }
    }
}
