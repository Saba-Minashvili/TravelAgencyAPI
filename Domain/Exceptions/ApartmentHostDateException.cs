namespace Domain.Exceptions
{
    public sealed class ApartmentHostDateException : BadRequestException
    {
        public ApartmentHostDateException(string message)
            :base(message)
        {
        }
    }
}
