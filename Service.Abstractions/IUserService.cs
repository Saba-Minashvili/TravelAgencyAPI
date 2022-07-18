using Contracts;

namespace Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserDto> GetByIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<RegisterUserDto> CreateAsync(RegisterUserDto userDto, CancellationToken cancellationToken = default);
        Task<UpdateUserDto> UpdateAsync(string userId, UpdateUserDto userDto, CancellationToken cancellationToken = default);
    }
}
