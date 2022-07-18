using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Book>> GetMyBookingsAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Bookings == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            return await _dbContext.Bookings
                .Where(o => o.GuestUserId == userId)
                .ToListAsync(cancellationToken);
        }

        public void AddBookAsync(Book book)
        {
            if (_dbContext.Bookings == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            _dbContext.Bookings.Add(book);
        }

        public void AcceptBookAsync(Book book)
        {
            if (_dbContext.Bookings == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            _dbContext.Bookings.Update(book);
        }

        public void DeclineBookAsync(Book book)
        {
            if (_dbContext.Bookings == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            _dbContext.Bookings.Update(book);
        }
    }
}
