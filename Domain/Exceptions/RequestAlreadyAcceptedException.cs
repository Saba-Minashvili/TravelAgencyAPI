namespace Domain.Exceptions
{
    public sealed class RequestAlreadyAcceptedException : BadRequestException
    {
        public RequestAlreadyAcceptedException(string message)
            :base(message)
        {
        }
    }
}
