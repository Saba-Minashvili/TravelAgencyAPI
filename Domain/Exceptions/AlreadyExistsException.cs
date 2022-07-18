namespace Domain.Exceptions
{
    public sealed class AlreadyExistsException : BadRequestException
    {
        public AlreadyExistsException(string property)
            :base($"User with this {property} already exists.")
        {

        }
    }
}
