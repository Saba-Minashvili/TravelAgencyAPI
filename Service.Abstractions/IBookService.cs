using Contracts;

namespace Services.Abstractions
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetMyBookingsAsync(string? userId, CancellationToken cancellationToken = default);
        Task AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default);
    }
}
