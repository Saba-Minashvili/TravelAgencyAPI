using Domain.Entities;

namespace Domain.Repositories
{
    public interface IBookRequestRepository
    {
        void SendBookRequestAsync(BookRequest bookRequest, CancellationToken cancellationToken = default);
    }
}
