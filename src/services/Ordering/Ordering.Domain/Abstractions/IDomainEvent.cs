using MediatR;

namespace Ordering.API.Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName!;
}
