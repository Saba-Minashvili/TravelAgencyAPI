namespace Domain.Exceptions
{
    public sealed class InvalidDateException:BadRequestException
    {
        public InvalidDateException()
            :base("Invalid date. The date must be in this {dd.MM.yyyy} format and must be in past.")
        {

        }
    }
}
