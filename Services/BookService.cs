using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Repositories;
using Encoder.Abstraction;
using Services.Abstractions;

namespace Services
{
    internal sealed class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEncodeService _encoder;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IEncodeService encoder)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _encoder = encoder;
        }

        public async Task<IEnumerable<BookDto>> GetMyBookingsAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("User id is null or empty.");
            }

            var myBookings = await _unitOfWork.BookRepository.GetMyBookingsAsync(userId, cancellationToken);
            
            var myBookingsDto = _mapper.Map<IEnumerable<BookDto>>(myBookings);

            foreach(var booking in myBookingsDto)
            {
                booking.ApartmentImage = _encoder.DecodeFromBase64(booking.ApartmentImage);
            }

            return myBookingsDto;
        }

        public async Task AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default)
        {
            var book = _mapper.Map<Book>(bookDto);

            _unitOfWork.BookRepository.AddBookAsync(book);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
