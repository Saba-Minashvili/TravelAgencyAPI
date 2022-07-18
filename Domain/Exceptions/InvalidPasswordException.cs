namespace Domain.Exceptions
{
    public sealed class InvalidPasswordException:BadRequestException
    {
        public InvalidPasswordException()
            :base("Invalid password. Password must be at least 7 characters and it must contain: Uppercase characters (A-Z)" +
                 "Lowercase characters (a-z), Digits (0-9), Special characters (~!@#$%^&*_-" +
                 "+=`|(){}[]:;<>,.?/)")
        {
        }
    }
}
