using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IApartmentService> _apartmentService;
        private readonly Lazy<IGuestService> _guestService;
        private readonly Lazy<IGuestRequestService> _guestRequestService;
        private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<IBookRequestService> _bookRequestService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _userService = new Lazy<IUserService>(() => new UserService(unitOfWork, mapper, userManager));
            _apartmentService = new Lazy<IApartmentService>(() => new ApartmentService(unitOfWork, mapper));
            _guestService = new Lazy<IGuestService>(() => new GuestService(unitOfWork, mapper));
            _guestRequestService = new Lazy<IGuestRequestService>(() => new GuestRequestService(unitOfWork));
            _bookService = new Lazy<IBookService>(() => new BookService(unitOfWork, mapper));
            _bookRequestService = new Lazy<IBookRequestService>(() => new BookRequestService(unitOfWork, mapper));
        }

        public IUserService UserService => _userService.Value;
        public IApartmentService ApartmentService => _apartmentService.Value;
        public IGuestService GuestService => _guestService.Value;
        public IGuestRequestService GuestRequestService => _guestRequestService.Value;
        public IBookService BookService => _bookService.Value;
        public IBookRequestService BookRequestService => _bookRequestService.Value;
    }
}
