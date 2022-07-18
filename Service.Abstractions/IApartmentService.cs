using Contracts;

namespace Services.Abstractions
{
    public interface IApartmentService
    {
        Task<IEnumerable<ApartmentDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ApartmentDto>> FilterByBedsNumber(int bedsNumber, CancellationToken cancellationToken = default);
        Task<IEnumerable<ApartmentDto>> SearchApartmentAsync(SearchApartmentDto searchApartmentDto, CancellationToken cancellationToken = default);
        Task<ApartmentDto> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<ApartmentDto> CreateAsync(ApartmentDto apartmentDto, CancellationToken cancellationToken = default);
        Task UpdateAsync(string userId, ApartmentDto apartmentDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(string userId, CancellationToken cancellationToken = default);
    }
}
