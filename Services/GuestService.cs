using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Repositories;
using Encoder.Abstraction;
using Services.Abstractions;

namespace Services
{
    internal sealed class GuestService : IGuestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEncodeService _encoder;

        public GuestService(IUnitOfWork unitOfWork, IMapper mapper, IEncodeService encoder)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _encoder = encoder;
        }

        public async Task<IEnumerable<GuestDto>> MyGuestsAsync(string hostUserId, CancellationToken cancellationToken)
        {
            var guests = await _unitOfWork.GuestRepository.MyGuestsAsync(hostUserId, cancellationToken);

            var guestsDto = _mapper.Map<IEnumerable<GuestDto>>(guests);

            foreach(var guest in guestsDto)
            {
                guest.UserPhoto = _encoder.DecodeFromBase64(guest.UserPhoto);
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

            guestDto.UserPhoto = _encoder.DecodeFromBase64(guest.UserPhoto);

            return guestDto;
        }

        public async Task<GuestDto> AddGuestAsync(GuestDto guestDto, CancellationToken cancellationToken = default)
        {
            var guest = _mapper.Map<Guest>(guestDto);

            _unitOfWork.GuestRepository.AddGuestAsync(guest);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return guestDto;
        }
    }
}
