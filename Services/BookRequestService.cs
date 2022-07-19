using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstractions;
using System.Globalization;

namespace Services
{
    internal sealed class BookRequestService : IBookRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SendBookRequestAsync(BookRequestDto bookRequestDto, CancellationToken cancellationToken = default)
        {
            // Getting 'MyBookings' to see if i already have that book request in it.
            var myBookings = await _unitOfWork.BookRepository.GetMyBookingsAsync(bookRequestDto.GuestUserId, cancellationToken);

            // Getting 'MyGuests' to see if i already have that book request in it.
            var myGuests = await _unitOfWork.GuestRepository.MyGuestsAsync(bookRequestDto.HostUserId, cancellationToken);

            // Not the best practice but for now i will leave it like this.
            var hostUsersApartment = await _unitOfWork.ApartmentRepository.GetByUserIdAsync(bookRequestDto.HostUserId, cancellationToken);

            if(hostUsersApartment == null)
            {
                throw new NullReferenceException("User apartment is null");
            }

            if (myBookings.FirstOrDefault(o => o.HostUserId == bookRequestDto.HostUserId) != null)
            {
                throw new RequestAlreadySentException("You have already sent request for that apartment.");
            }
            if (Convert.ToDateTime(bookRequestDto.From) < Convert.ToDateTime(hostUsersApartment.From)
                || Convert.ToDateTime(bookRequestDto.To) > Convert.ToDateTime(hostUsersApartment.To))
            {
                throw new ApartmentHostDateException($"Apartment is only available from {hostUsersApartment.From} to {hostUsersApartment.To}," +
                    $"You can't book that apartment from {bookRequestDto.From} to {bookRequestDto.To}.");
            }
            if (IsApartmentAvailable(hostUsersApartment, bookRequestDto) == false)
            {
                throw new ApartmentHostDateException($"Apartment is unavailable from {hostUsersApartment.IsTakenFrom} to {hostUsersApartment.IsTakenTo}" +
                    $". You can't book apartment from {bookRequestDto.From} to {bookRequestDto.To}");
            }

            // This is the user who sents the book request
            var userForGuest = await _unitOfWork.UserRepository.GetByIdAsync(bookRequestDto.GuestUserId, cancellationToken);

            if(userForGuest == null)
            {
                throw new NullReferenceException("User is null");
            }

            var guest = new Guest { 
                UserPhoto = userForGuest.Photo,
                FirstName = userForGuest.FirstName, 
                LastName = userForGuest.LastName, 
                From = ConvertToValidDateFormat(bookRequestDto.From),
                To = ConvertToValidDateFormat(bookRequestDto.To),
                Description = userForGuest.Description, 
                GuestUserId = userForGuest.Id, 
                HostUserId = bookRequestDto.HostUserId 
            };

            _unitOfWork.GuestRepository.AddGuestAsync(guest);

            // This is the user of apartment, who is sent the book request to
            var userForBook = await _unitOfWork.UserRepository.GetByIdAsync(bookRequestDto.HostUserId, cancellationToken);

            if(userForBook == null)
            {
                throw new NullReferenceException("User is null");
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var book = new Book
            {
                City = userForBook.Apartment.City,
                DistanceToCenter = userForBook.Apartment.DistanceToCenter,
                BedsNumber = userForBook.Apartment.BedsNumber,
                ApartmentDescription = userForBook.Apartment.Description,
                ApartmentImage = userForBook.Apartment.ImageBase64,
                From = ConvertToValidDateFormat(bookRequestDto.From),
                To = ConvertToValidDateFormat(bookRequestDto.To),
                GuestUserId = bookRequestDto.GuestUserId,
                HostUserId = userForBook.Id,
                IsPending = true,
                IsAccepted = false
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _unitOfWork.BookRepository.AddBookAsync(book);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private bool IsApartmentAvailable(Apartment apartment, BookRequestDto bookRequest)
        {
            var isAvailableFrom = Convert.ToDateTime(apartment.From);
            var isAvailableTo = Convert.ToDateTime(apartment.To);

            if(apartment.IsTaken == true)
            {
                var isTakenFrom = Convert.ToDateTime(apartment.IsTakenFrom);
                var isTakenTo = Convert.ToDateTime(apartment.IsTakenTo);

                if (Convert.ToDateTime(isAvailableFrom) != Convert.ToDateTime(isTakenFrom))
                {
                    if (Convert.ToDateTime(bookRequest.From) >= Convert.ToDateTime(isAvailableFrom)
                        && Convert.ToDateTime(bookRequest.From) < Convert.ToDateTime(isTakenFrom)
                        && Convert.ToDateTime(bookRequest.To) < Convert.ToDateTime(isTakenFrom))
                    {
                        return true;
                    }else if(Convert.ToDateTime(bookRequest.From) > Convert.ToDateTime(isTakenTo)
                        && Convert.ToDateTime(bookRequest.From) < Convert.ToDateTime(isAvailableTo)
                        && Convert.ToDateTime(bookRequest.To) <= Convert.ToDateTime(isAvailableTo))
                    {
                        return true;
                    }

                    return false;
                }

                if (Convert.ToDateTime(isAvailableFrom) == Convert.ToDateTime(isTakenFrom) && Convert.ToDateTime(isTakenFrom) < Convert.ToDateTime(isAvailableTo))
                {
                    if (Convert.ToDateTime(bookRequest.From) > Convert.ToDateTime(isTakenTo)
                        && Convert.ToDateTime(bookRequest.To) <= Convert.ToDateTime(isAvailableTo))
                    {
                        return true;
                    }

                    return false;
                }

                // Means that apartment is not available.
                return false;
            }else
            {
                // It means that apartment is available.
                return true;
            }
        }

        private string? ConvertToValidDateFormat(string? dateTime)
        {
            string[] dateFormats = {"yyyy-MM-dd","dd-MM-yyyy", "MM-dd-yyyy",
                "yyyy.MM.dd", "dd.MM.yyyy", "MM.dd.yyyy",
                "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd", "yyyy/dd/MM"};

            DateTime? dt = DateTime.ParseExact(dateTime, dateFormats, CultureInfo.InvariantCulture);

            string result = dt.Value.ToString("dd.MM.yyyy");

            return result;
        }
    }
}
