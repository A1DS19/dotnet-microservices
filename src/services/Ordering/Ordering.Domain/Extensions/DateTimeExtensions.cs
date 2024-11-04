namespace Ordering.API.Ordering.Domain.Extensions;

public static class DateTimeExtensions
{
    public static void ThrowIfExpired(this DateTime dateTime)
    {
        if (dateTime < DateTime.UtcNow)
        {
            throw new DomainException("The expiration date is invalid.");
        }
    }
}
