namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string Street { get; init; } = default!;
    public string City { get; init; } = default!;
    public string State { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string ZipCode { get; init; } = default!;

    protected Address() { }

    private Address(string Street, string City, string State, string Country, string ZipCode)
    {
        this.Street = Street;
        this.City = City;
        this.State = State;
        this.Country = Country;
        this.ZipCode = ZipCode;
    }

    public static Address Of(
        string street,
        string city,
        string state,
        string country,
        string zipCode
    )
    {
        ArgumentException.ThrowIfNullOrEmpty(street, nameof(street));
        ArgumentException.ThrowIfNullOrEmpty(city, nameof(city));
        ArgumentException.ThrowIfNullOrEmpty(state, nameof(state));
        ArgumentException.ThrowIfNullOrEmpty(country, nameof(country));
        ArgumentException.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));

        return new Address(street, city, state, country, zipCode);
    }
}
