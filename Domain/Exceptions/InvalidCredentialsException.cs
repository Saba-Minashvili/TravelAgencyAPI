namespace Domain.Exceptions
{
    public sealed class InvalidCredentialsException:BadRequestException
    {
        public InvalidCredentialsException()
            :base("Invalid Login or Password. Please try again.")
        {

        }
    }
}
