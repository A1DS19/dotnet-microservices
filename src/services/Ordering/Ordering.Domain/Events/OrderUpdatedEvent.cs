namespace Ordering.API.Ordering.Domain.Events;

public record OrderUpdatedEvent(Order Order) : IDomainEvent;
