namespace Ordering.Domain.Extensions;

public static class DateTimeExtensions
{
    public static void ThrowIfExpired(this DateTime dateTime)
    {
        if (dateTime < DateTime.Today)
        {
            throw new DomainException("The expiration date is invalid.");
        }
    }
}
