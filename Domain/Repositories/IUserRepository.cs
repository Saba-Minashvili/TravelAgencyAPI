using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(string? userId, CancellationToken cancellationToken = default);
        void CreateAsync(User user);
        void UpdateAsync(string? userId, User user);
    }
}
