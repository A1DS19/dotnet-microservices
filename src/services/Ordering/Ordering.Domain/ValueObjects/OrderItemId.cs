namespace Ordering.API.Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid guid)
    {
        ArgumentNullException.ThrowIfNull(guid, nameof(guid));
        if (guid == Guid.Empty)
        {
            throw new DomainException("Order item id cannot be empty.");
        }

        return new OrderItemId(guid);
    }
}
