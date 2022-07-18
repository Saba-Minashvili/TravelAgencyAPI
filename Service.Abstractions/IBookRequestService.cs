using Contracts;

namespace Services.Abstractions
{
    public interface IBookRequestService
    {
        Task SendBookRequestAsync(BookRequestDto bookRequestDto, CancellationToken cancellationToken = default);
    }
}
