namespace Domain.Exceptions
{
    public sealed class ApartmentAlreadyTakenException : BadRequestException
    {
        public ApartmentAlreadyTakenException(string message)
            :base(message)
        {
        }
    }
}
