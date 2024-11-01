namespace Ordering.API.Ordering.Domain.ValueObjects;

public record Address
{
    public string Street { get; init; } = default!;
    public string City { get; init; } = default!;
    public string State { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string ZipCode { get; init; } = default!;
}
