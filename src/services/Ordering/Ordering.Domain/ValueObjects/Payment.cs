namespace Ordering.API.Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardNumber { get; init; } = default!;
    public string CardHolderName { get; init; } = default!;
    public DateTime Expiration { get; init; }
    public string SecurityNumber { get; init; } = default!;
}
