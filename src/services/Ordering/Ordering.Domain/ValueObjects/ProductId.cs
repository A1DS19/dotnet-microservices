namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }

    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid guid)
    {
        ArgumentNullException.ThrowIfNull(guid, nameof(guid));
        if (guid == Guid.Empty)
        {
            throw new DomainException("Product id cannot be empty.");
        }

        return new ProductId(guid);
    }
}
