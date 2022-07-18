using Domain.Entities;

namespace Domain.Repositories
{
    public interface IApartmentRepository
    {
        Task<IEnumerable<Apartment>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Apartment>> FilterByBedsNumber(int bedsNumber, CancellationToken cancellationToken = default);
        Task<Apartment?> GetByUserIdAsync(string? userId, CancellationToken cancellationToken = default);
        void CreateAsync(Apartment apartment);
        void UpdateAsync(int apartmentId, Apartment apartment);
        void DeleteAsync(Apartment apartment);
    }
}
