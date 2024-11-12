namespace Ordering.Application.EventHandlers.Domain;

public class OrderUpdatedEventHandler(
    IPublishEndpoint publish,
    ILogger<OrderUpdatedEventHandler> logger
) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Domain Event {domainEvent.GetType().Name} handled.");

        return Task.CompletedTask;
    }
}
