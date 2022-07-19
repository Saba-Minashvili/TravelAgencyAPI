using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    internal sealed class GuestRequestService : IGuestRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GuestRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AcceptGuestRequestAsync(string? guestUserId, string? hostUserId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(guestUserId) || string.IsNullOrEmpty(hostUserId))
            {
                throw new ArgumentNullException("User id is null or empty.");
            }
            // This guest is need for following reason: when host user declines or accepts particular guests,
            // it should be removed from 'MyGuests' lists
            var guest = await _unitOfWork.GuestRepository.GetGuestByIdAsync(guestUserId, cancellationToken);

            if(guest == null)
            {
                throw new NullReferenceException("Guest user is null");
            }

            var host = await _unitOfWork.UserRepository.GetByIdAsync(hostUserId, cancellationToken);

            if(host == null)
            {
                throw new NullReferenceException("Host user is null");
            }

            // List of bookings of particular guest (guest with 'guestUserId' Id)
            var guestBookings = await _unitOfWork.BookRepository.GetMyBookingsAsync(guestUserId, cancellationToken);

            if(guestBookings == null)
            {
                throw new NullReferenceException("Data of bookings is null");
            }

            // Particular booking of that guest.
            var guestBooking = guestBookings.FirstOrDefault(o => o.HostUserId == hostUserId);

            if(guestBooking == null)
            {
                throw new NullReferenceException("Booking of guest is null");
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (host.Apartment.IsTaken == true 
                && (Convert.ToDateTime(guestBooking.From) >= Convert.ToDateTime(host.Apartment.IsTakenFrom)
                && Convert.ToDateTime(guestBooking.From) <= Convert.ToDateTime(host.Apartment.IsTakenTo)
                && Convert.ToDateTime(guestBooking.To) <= Convert.ToDateTime(host.Apartment.IsTakenTo)))
            {
                throw new ApartmentAlreadyTakenException($"This apartment is already taken from {host.Apartment.IsTakenFrom} to {host.Apartment.IsTakenTo}" +
                    $". You can't accept two guests for one apartment .");
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (guestBooking.IsAccepted == true && guestBooking.IsPending == false)
            {
                throw new RequestAlreadyAcceptedException("Request is already accepted.");
            }
            if (guestBooking.IsPending == false && guestBooking.IsAccepted == false)
            {
                throw new RequestAlreadyDeclinedException("Request is already declined. You can't approve declined request.");
            }

            guestBooking.IsPending = false;
            guestBooking.IsAccepted = true;

            host.Apartment.IsTaken = true;
            host.Apartment.IsTakenFrom = guestBooking.From;
            host.Apartment.IsTakenTo = guestBooking.To;

            _unitOfWork.GuestRequestRepository.AcceptGuestRequestAsync(guestBooking, guest);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeclineGuestRequestAsync(string? guestUserId, string? hostUserId, CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrEmpty(guestUserId) || string.IsNullOrEmpty(hostUserId))
            {
                throw new ArgumentNullException("User id is null or empty.");
            }

            // This guest is need for following reason: when host user declines or accepts particular guests,
            // it should be removed from 'MyGuests' lists
            var guest = await _unitOfWork.GuestRepository.GetGuestByIdAsync(guestUserId, cancellationToken);

            if(guest == null)
            {
                throw new NullReferenceException("Guest user is null");
            }

            var guestBookings = await _unitOfWork.BookRepository.GetMyBookingsAsync(guestUserId, cancellationToken);

            if(guestBookings == null)
            {
                throw new NullReferenceException("Data of booking is null");
            }

            var guestBooking = guestBookings.FirstOrDefault(o => o.HostUserId == hostUserId);

            if(guestBooking == null)
            {
                throw new NullReferenceException("Booking of guest is null");
            }

            if (guestBooking.IsAccepted == true && guestBooking.IsPending == false)
            {
                throw new RequestAlreadyAcceptedException("Request is already accepted. You can't decline approved request.");
            }
            if (guestBooking.IsPending == false && guestBooking.IsAccepted == false)
            {
                throw new RequestAlreadyDeclinedException("Request is already declined.");
            }

            guestBooking.IsPending = false;
            guestBooking.IsAccepted = false;

            _unitOfWork.GuestRequestRepository.DeclineGuestRequestAsync(guestBooking, guest);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
