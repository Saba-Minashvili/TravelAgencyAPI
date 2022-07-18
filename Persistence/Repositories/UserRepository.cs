using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContext.Users == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            return await _dbContext.Users
                .Include(o => o.Apartment)
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Users == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }
            
            return await _dbContext.Users
                .Include(o => o.Apartment)
                .FirstOrDefaultAsync(o => o.Id == userId, cancellationToken);
        }

        public void CreateAsync(User user)
        {
            if (_dbContext.Users == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            _dbContext.Users.Add(user);
        }

        public void UpdateAsync(string? userId, User user)
        {
            if (_dbContext.Users == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Users));
            }

            user.Id = userId;
            _dbContext.Users.Update(user);
        }

    }
}
