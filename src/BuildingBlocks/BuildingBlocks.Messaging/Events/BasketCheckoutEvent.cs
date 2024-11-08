namespace BuildingBlocks.Messaging.Events;

public record BasketCheckoutEvent : IntegrationEvent
{
    public string Username { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;

    // Billing address and shipping address
    public string Street { get; init; } = default!;
    public string City { get; init; } = default!;
    public string State { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string ZipCode { get; init; } = default!;

    // Payment
    public string CardNumber { get; init; } = default!;
    public string CardHolderName { get; init; } = default!;
    public DateTime Expiration { get; init; }
    public string SecurityNumber { get; init; } = default!;
}
