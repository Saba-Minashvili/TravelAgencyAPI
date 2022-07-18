namespace Domain.Exceptions
{
    public sealed class RequestAlreadySentException:BadRequestException
    {
        public RequestAlreadySentException(string message)
            :base(message)
        {
        }
    }
}
