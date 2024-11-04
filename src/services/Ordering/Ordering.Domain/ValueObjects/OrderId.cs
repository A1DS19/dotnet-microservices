namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid guid)
    {
        ArgumentNullException.ThrowIfNull(guid, nameof(guid));
        if (guid == Guid.Empty)
        {
            throw new DomainException("Order id cannot be empty.");
        }

        return new OrderId(guid);
    }
}
