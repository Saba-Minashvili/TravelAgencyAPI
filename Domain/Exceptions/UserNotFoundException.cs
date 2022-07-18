namespace Domain.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string property)
            :base($"User with this {property} was not found.")
        {

        }
    }
}
