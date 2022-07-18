namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IApartmentService ApartmentService { get; }
        IGuestService GuestService { get; }
        IGuestRequestService GuestRequestService { get; }
        IBookService BookService { get; }
        IBookRequestService BookRequestService { get; }
    }
}
