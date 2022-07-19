using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Encoder.Abstraction;
using Services.Abstractions;
using System.Globalization;

namespace Services
{
    internal sealed class ApartmentService : IApartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEncodeService _encoder;

        public ApartmentService(IUnitOfWork unitOfWork, IMapper mapper, IEncodeService encoder)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _encoder = encoder;
        }

        public async Task<IEnumerable<ApartmentDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var apartments = await _unitOfWork.ApartmentRepository.GetAllAsync(cancellationToken);

            if(apartments == null)
            {
                throw new NullReferenceException("Data of apartments is null.");
            }

            var apartmentsDto = _mapper.Map<IEnumerable<ApartmentDto>>(apartments);

            apartmentsDto = ReturnValidCollection(apartmentsDto);

            return apartmentsDto;
        }

        public async Task<IEnumerable<ApartmentDto>> FilterByBedsNumber(int bedsNumber, CancellationToken cancellationToken = default)
        {
            var apartments = await _unitOfWork.ApartmentRepository.FilterByBedsNumber(bedsNumber, cancellationToken);

            if (apartments == null)
            {
                throw new NullReferenceException("Data of apartments is null.");
            }

            var apartmentsDto = _mapper.Map<IEnumerable<ApartmentDto>>(apartments);

            apartmentsDto = ReturnValidCollection(apartmentsDto);

            return apartmentsDto;
        }

        public async Task<IEnumerable<ApartmentDto>> SearchApartmentAsync(SearchApartmentDto searchApartmentDto, CancellationToken cancellationToken = default)
        {
            string? convertedFrom = ConvertToValidDateFormat(searchApartmentDto.From);
            string? convertedTo = ConvertToValidDateFormat(searchApartmentDto.To);

            var apartments = await _unitOfWork.ApartmentRepository.GetAllAsync(cancellationToken);

            if (apartments == null)
            {
                throw new NullReferenceException("Data of apartments is null.");
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var filteredApartments = apartments
                .Where(o => Convert.ToDateTime(o.From) >= Convert.ToDateTime(convertedFrom)
                       && Convert.ToDateTime(o.To) <= Convert.ToDateTime(convertedTo)
                       && o.City.ToLower() == searchApartmentDto.City.ToLower());
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var apartmentsDto = _mapper.Map<IEnumerable<ApartmentDto>>(filteredApartments);
            
            apartmentsDto = ReturnValidCollection(apartmentsDto);

            return apartmentsDto;
        }

        public async Task<ApartmentDto> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var apartment = await _unitOfWork.ApartmentRepository.GetByUserIdAsync(userId);

            if (apartment == null)
            {
                throw new NullReferenceException("apartment is null.");
            }


            var apartmentDto = _mapper.Map<ApartmentDto>(apartment);

            apartmentDto.ImageBase64 = _encoder.DecodeFromBase64(apartment.ImageBase64);

            return apartmentDto;
        }

        public async Task<ApartmentDto> CreateAsync(ApartmentDto apartmentDto, CancellationToken cancellationToken = default)
        {
            var apartment = _mapper.Map<Apartment>(apartmentDto);

            if (IsValidAvailabilityDate(apartmentDto.From, apartmentDto.To) == false)
            {
                throw new ApartmentAvailabilityDateException("Invalid 'From' and 'To' dates. They cannot be equal or be in the past, the must be in the future.");
            }
            apartment.ImageBase64 = _encoder.EncodeToBase64(apartment.ImageBase64);
            apartment.From = ConvertToValidDateFormat(apartmentDto.From);
            apartment.To = ConvertToValidDateFormat(apartmentDto.To);
            apartment.IsTaken = false;
            apartment.IsTakenFrom = "";
            apartment.IsTakenTo = "";

            _unitOfWork.ApartmentRepository.CreateAsync(apartment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return apartmentDto;
        }

        public async Task DeleteAsync(string userId, CancellationToken cancellationToken = default)
        {
            var apartment = await _unitOfWork.ApartmentRepository.GetByUserIdAsync(userId, cancellationToken);

            if (apartment == null)
            {
                throw new NullReferenceException("Apartment is null.");
            }

            _unitOfWork.ApartmentRepository.DeleteAsync(apartment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(string userId, ApartmentDto apartmentDto, CancellationToken cancellationToken = default)
        {
            var apartment = await _unitOfWork.ApartmentRepository.GetByUserIdAsync(userId, cancellationToken);

            if (apartment == null)
            {
                throw new NullReferenceException("Apartment is null.");
            }

            if (apartment.IsTaken == true)
            {
                throw new ApartmentAlreadyTakenException("You can't update your apartment, while it is taken by guest.");
            }
            if (apartmentDto.From != apartment.From && apartmentDto.To != apartment.To && !IsValidAvailabilityDate(apartmentDto.From, apartmentDto.To))
            {
                throw new ApartmentAvailabilityDateException("Invalid 'From' and 'To' dates. They cannot be equal or be in the past, the must be in the future.");
            }

            apartment.Address = apartmentDto.Address;
            apartment.BedsNumber = apartmentDto.BedsNumber;
            apartment.City = apartmentDto.City;
            apartment.DistanceToCenter = apartmentDto.DistanceToCenter;
            apartment.ImageBase64 = _encoder.EncodeToBase64(apartmentDto.ImageBase64);
            apartment.From = ConvertToValidDateFormat(apartmentDto.From);
            apartment.To = ConvertToValidDateFormat(apartmentDto.To);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
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

        private bool IsValidAvailabilityDate(string? from, string? to)
        {
            string[] dateFormats = {"yyyy-MM-dd","dd-MM-yyyy", "MM-dd-yyyy",
                "yyyy.MM.dd", "dd.MM.yyyy", "MM.dd.yyyy",
                "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd", "yyyy/dd/MM"};

            DateTime dtFrom = DateTime.ParseExact(from, dateFormats, CultureInfo.InvariantCulture);
            DateTime dtTo = DateTime.ParseExact(to, dateFormats, CultureInfo.InvariantCulture);
            int dtFromCompare = DateTime.Compare(dtFrom, DateTime.UtcNow);
            int dtToCompare = DateTime.Compare(dtTo, DateTime.UtcNow);
            int dtCompare = DateTime.Compare(dtTo, dtFrom);

            if (dtFromCompare < 0 || dtToCompare < 0)
            {
                return false;
            }
            if(dtCompare == 0)
            {
                return false;
            }

            return true;
        }

        // This method used for apartment list to convert dates and decode images
        // every time we make a call for get apartments request
        private IEnumerable<ApartmentDto> ReturnValidCollection(IEnumerable<ApartmentDto> collection)
        {
            foreach (var apartment in collection)
            {
                if(apartment.IsTaken == false)
                {
                    apartment.IsTakenFrom = "";
                    apartment.IsTakenTo = "";
                }

                apartment.ImageBase64 = _encoder.DecodeFromBase64(apartment.ImageBase64);
            }

            return collection;
        }
    }
}
