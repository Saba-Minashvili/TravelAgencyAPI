using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    internal sealed class GuestService : IGuestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GuestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GuestDto>> MyGuestsAsync(string hostUserId, CancellationToken cancellationToken)
        {
            var guests = await _unitOfWork.GuestRepository.MyGuestsAsync(hostUserId, cancellationToken);

            var guestsDto = _mapper.Map<IEnumerable<GuestDto>>(guests);

            foreach(var guest in guestsDto)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                guest.UserPhoto = DecodeFrom64(guest.UserPhoto);
#pragma warning restore CS8604 // Possible null reference argument.
            }

            return guestsDto;
        }

        public async Task<GuestDto> GetGuestByIdAsync(string? guestUserId, CancellationToken cancellationToken = default)
        {
            var guest = await _unitOfWork.GuestRepository.GetGuestByIdAsync(guestUserId, cancellationToken);

            if(guest == null)
            {
                throw new NullReferenceException(nameof(guest));
            }

            var guestDto = _mapper.Map<GuestDto>(guest);

#pragma warning disable CS8604 // Possible null reference argument.
            guestDto.UserPhoto = DecodeFrom64(guest.UserPhoto);
#pragma warning restore CS8604 // Possible null reference argument.

            return guestDto;
        }

        public async Task<GuestDto> AddGuestAsync(GuestDto guestDto, CancellationToken cancellationToken = default)
        {
            var guest = _mapper.Map<Guest>(guestDto);

            _unitOfWork.GuestRepository.AddGuestAsync(guest);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return guestDto;
        }

        private string DecodeFrom64(string encodedString)
        {
            byte[] encodedStringAsBytes = System.Convert.FromBase64String(encodedString);
            string result = System.Text.ASCIIEncoding.ASCII.GetString(encodedStringAsBytes);

            if (result != null)
            {
                return result;
            }

            return "";
        }
    }
}
