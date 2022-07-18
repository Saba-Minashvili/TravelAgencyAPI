namespace Domain.Exceptions
{
    public sealed class RequestAlreadyDeclinedException : BadRequestException
    {
        public RequestAlreadyDeclinedException(string message)
            :base(message)
        {
        }
    }
}
