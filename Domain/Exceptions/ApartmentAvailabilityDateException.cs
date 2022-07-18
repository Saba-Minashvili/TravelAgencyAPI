namespace Domain.Exceptions
{
    public sealed class ApartmentAvailabilityDateException : BadRequestException
    {
        public ApartmentAvailabilityDateException(string message)
            :base(message)
        {
        }
    }
}
