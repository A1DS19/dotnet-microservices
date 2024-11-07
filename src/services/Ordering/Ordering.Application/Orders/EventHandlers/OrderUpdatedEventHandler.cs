namespace Ordering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler(ILogger logger) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Domain Event {notification.GetType().Name} handled.");

        return Task.CompletedTask;
    }
}