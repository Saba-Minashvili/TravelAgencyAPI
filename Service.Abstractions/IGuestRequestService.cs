namespace Services.Abstractions
{
    public interface IGuestRequestService
    {
        Task AcceptGuestRequestAsync(string? guestUserId, string? hostUserId, CancellationToken cancellationToken = default);
        Task DeclineGuestRequestAsync(string? guestUserId, string? hostUserId, CancellationToken cancellationToken = default);
    }
}
