namespace Ordering.API.Ordering.Domain.Events;

public record OrderCreatedEvent(Order Order) : IDomainEvent;
