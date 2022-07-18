namespace Domain.Exceptions
{
    public sealed class EmptyPropertyException:BadRequestException
    {
        public EmptyPropertyException(string message)
            :base(message)
        {
        }
    }
}
