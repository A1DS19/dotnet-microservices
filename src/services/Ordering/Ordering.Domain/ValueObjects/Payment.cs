namespace Ordering.Domain.ValueObjects;

public record Payment
{
    private const int CardNumberLength = 16;
    private const int SecurityNumberLength = 3;

    public string CardNumber { get; init; } = default!;
    public string CardHolderName { get; init; } = default!;
    public DateTime Expiration { get; init; }
    public string SecurityNumber { get; init; } = default!;

    protected Payment() { }

    private Payment(
        string cardNumber,
        string cardHolderName,
        DateTime expiration,
        string securityNumber
    )
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        Expiration = expiration;
        SecurityNumber = securityNumber;
    }

    public static Payment Of(
        string cardNumber,
        string cardHolderName,
        DateTime expiration,
        string securityNumber
    )
    {
        ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
        ArgumentOutOfRangeException.ThrowIfNotEqual(
            cardNumber.Length,
            CardNumberLength,
            nameof(cardNumber)
        );
        ArgumentException.ThrowIfNullOrEmpty(cardHolderName, nameof(cardHolderName));
        ArgumentException.ThrowIfNullOrEmpty(securityNumber, nameof(securityNumber));
        ArgumentOutOfRangeException.ThrowIfNotEqual(
            securityNumber.Length,
            SecurityNumberLength,
            nameof(securityNumber)
        );
        expiration.ThrowIfExpired();

        return new Payment(cardNumber, cardHolderName, expiration, securityNumber);
    }
}
