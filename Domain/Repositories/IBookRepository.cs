using Domain.Entities;

namespace Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetMyBookingsAsync(string? userId, CancellationToken cancellationToken = default);
        void AddBookAsync(Book book);
        void AcceptBookAsync(Book book);
        void DeclineBookAsync(Book book);
    }
}
