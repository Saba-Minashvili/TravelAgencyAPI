using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    internal sealed class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetMyBookingsAsync(string? userId, CancellationToken cancellationToken = default)
        {
            var myBookings = await _unitOfWork.BookRepository.GetMyBookingsAsync(userId, cancellationToken);
            
            var myBookingsDto = _mapper.Map<IEnumerable<BookDto>>(myBookings);

            foreach(var booking in myBookingsDto)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                booking.ApartmentImage = DecodeFrom64(booking.ApartmentImage);
#pragma warning restore CS8604 // Possible null reference argument.
            }

            return myBookingsDto;
        }

        public async Task AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default)
        {
            var book = _mapper.Map<Book>(bookDto);

            _unitOfWork.BookRepository.AddBookAsync(book);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
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
