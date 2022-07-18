namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IApartmentRepository ApartmentRepository { get; }
        IGuestRepository GuestRepository { get; }
        IGuestRequestRepository GuestRequestRepository { get; }
        IBookRepository BookRepository { get; }
        IBookRequestRepository BookRequestRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
