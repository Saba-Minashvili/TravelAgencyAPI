using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            UserRepository = new UserRepository(_dbContext);
            ApartmentRepository = new ApartmentRepository(_dbContext);
            GuestRepository = new GuestRepository(_dbContext);
            GuestRequestRepository = new GuestRequestRepository(_dbContext);
            BookRepository = new BookRepository(_dbContext);
            BookRequestRepository = new BookRequestRepository(_dbContext);
        }

        public IUserRepository UserRepository { get; private set; }
        public IApartmentRepository ApartmentRepository { get; private set; }
        public IGuestRepository GuestRepository { get; private set; }
        public IGuestRequestRepository GuestRequestRepository { get; private set; }
        public IBookRepository BookRepository { get; private set; }
        public IBookRequestRepository BookRequestRepository { get; private set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
