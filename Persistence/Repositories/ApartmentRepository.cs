using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class ApartmentRepository : IApartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApartmentRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Apartment>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContext.Apartments == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            return await _dbContext.Apartments
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Apartment>> FilterByBedsNumber(int bedsNumber, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Apartments == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            return await _dbContext.Apartments
                .Where(o => o.BedsNumber == bedsNumber)
                .ToListAsync(cancellationToken);
        }

        public async Task<Apartment?> GetByUserIdAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Apartments == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            return await _dbContext.Apartments
                .FirstOrDefaultAsync(o => o.HostUserId == userId, cancellationToken);
        }

        public void CreateAsync(Apartment apartment)
        {
            if (_dbContext.Apartments == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            _dbContext.Apartments.Add(apartment);
        }

        public void DeleteAsync(Apartment apartment)
        {
            if (_dbContext.Apartments == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            _dbContext.Apartments.Remove(apartment);
        }

        public void UpdateAsync(int apartmentId, Apartment apartment)
        {
            if (_dbContext.Apartments == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            apartment.Id = apartmentId;
            _dbContext.Apartments.Update(apartment);
        }
    }
}
