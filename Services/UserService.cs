using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Encoder.Abstraction;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEncodeService _encoder;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IEncodeService encoder, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _encoder = encoder;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync(cancellationToken);

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            foreach (var user in usersDto)
            {
                user.Photo = _encoder.DecodeFromBase64(user.Photo);
            }

            return usersDto;
        }

        public async Task<UserDto> GetByIdAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken);

            if(user == null)
            {
                throw new UserNotFoundException("Id");
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Photo = _encoder.DecodeFromBase64(user.Photo);
            
            return userDto;
        }

        public async Task<RegisterUserDto> CreateAsync(RegisterUserDto userDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);

                if (await ValidatePasswordAsync(user, userDto.Password) == false)
                {
                    throw new InvalidPasswordException();
                }
                if(await CheckDuplicateName(user.Login) == true)
                {
                    throw new AlreadyExistsException("Login");
                }
                if(await CheckDuplicateEmail(user.Email) == true)
                {
                    throw new AlreadyExistsException("Email");
                }
                
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.UserName = userDto.Login;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userDto.Password);
                user.Photo = "";
                user.Description = "";

                await _userManager.CreateAsync(user);

                return userDto;
            }
            catch(ArgumentNullException)
            {
                if(userDto == null)
                {
                    throw new ArgumentNullException(nameof(userDto));
                }
            }

            return userDto;
        }

        public async Task<UpdateUserDto> UpdateAsync(string userId, UpdateUserDto userDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId));
                }

                var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (userDto.Login.ToLower() != user.Login.ToLower() && await CheckDuplicateName(userDto.Login) == true)
                {
                    throw new AlreadyExistsException("Login");
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (userDto.Email.ToLower() != user.Email.ToLower() && await CheckDuplicateEmail(userDto.Email) == true)
                {
                    throw new AlreadyExistsException("Email");
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (userDto.FirstName == "")
                {
                    throw new EmptyPropertyException("FirstName property cannot be emtpy.");
                }

                if(userDto.LastName == "")
                {
                    throw new EmptyPropertyException("LastName property cannot be empty.");
                }

                if(userDto.Email == "")
                {
                    throw new EmptyPropertyException("Email property cannot be empty.");
                }

                if(userDto.Photo == null)
                {
                    user.Photo = "";
                }else
                {
                    user.Photo = _encoder.EncodeToBase64(userDto.Photo);
                }

                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;
                // Manually updating 'NormalizedEmail' property of user
                user.NormalizedEmail = userDto.Email.ToUpper();
                user.Login = userDto.Login;
                user.UserName = userDto.Login;
                // Manually updating 'NormalizedUserName' property of user
                user.NormalizedUserName = userDto.Login.ToUpper();
                user.Description = userDto.Description;

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return userDto;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(nameof(userDto));
            }
        }

        private async Task<bool> ValidatePasswordAsync(User user, string? password)
        {
            IdentityResult result;

            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            foreach (IPasswordValidator<User> passwordValidator in _userManager.PasswordValidators)
            {
                result = await passwordValidator.ValidateAsync(_userManager, user, password);

                if (!result.Succeeded)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> CheckDuplicateName(string? login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return false;
            }

            var result = await _userManager.FindByNameAsync(login);

            return result != null;
        }

        private async Task<bool> CheckDuplicateEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            var result = await _userManager.FindByEmailAsync(email);

            return result != null;
        }
    }
}
