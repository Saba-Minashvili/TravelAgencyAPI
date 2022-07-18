using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync(cancellationToken);

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            foreach (var user in usersDto)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                user.Photo = DecodeFrom64(user.Photo);
#pragma warning restore CS8604 // Possible null reference argument.
            }

            return usersDto;
        }

        public async Task<UserDto> GetByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken);

            if(user == null)
            {
                throw new UserNotFoundException("Id");
            }

            var userDto = _mapper.Map<UserDto>(user);
#pragma warning disable CS8604 // Possible null reference argument.
            userDto.Photo = DecodeFrom64(userDto.Photo);
#pragma warning restore CS8604 // Possible null reference argument.
            
            return userDto;
        }

        public async Task<RegisterUserDto> CreateAsync(RegisterUserDto userDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
#pragma warning disable CS8604 // Possible null reference argument.
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
#pragma warning restore CS8604 // Possible null reference argument.
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
                    user.Photo = EncodeTo64(userDto.Photo);
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

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

        private async Task<bool> ValidatePasswordAsync(User user, string password)
        {
            IdentityResult result;
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

        private async Task<bool> CheckDuplicateName(string login)
        {
            var result = await _userManager.FindByNameAsync(login);
            if (result != null)
            {
                // Means that User with similar Login was found.
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> CheckDuplicateEmail(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if(result != null)
            {
                // Means that User with similar email was found.
                return true;
            }else
            {
                return false;
            }
        }

        private string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string result = System.Convert.ToBase64String(toEncodeAsBytes);

            if (result != null)
            {
                return result;
            }

            return "";
        }

        private string DecodeFrom64(string encodedString)
        {
            byte[] encodedStringAsBytes = System.Convert.FromBase64String(encodedString);
            string result = System.Text.ASCIIEncoding.ASCII.GetString(encodedStringAsBytes);

            if (result != null)
            {
                return result;
            }

            return "";
        }
    }
}
