using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    internal sealed class BookRequestRepository : IBookRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRequestRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public void SendBookRequestAsync(BookRequest bookRequest, CancellationToken cancellationToken = default)
        {
            if (_dbContext.BookRequests == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            _dbContext.BookRequests.Add(bookRequest);
        }
    }
}
