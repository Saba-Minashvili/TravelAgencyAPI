using Domain.Entities;

namespace Domain.Repositories
{
    public interface IGuestRequestRepository
    {
        void AcceptGuestRequestAsync(Book guestBooking, Guest guest);
        void DeclineGuestRequestAsync(Book guestBooking, Guest guest);
    }
}
