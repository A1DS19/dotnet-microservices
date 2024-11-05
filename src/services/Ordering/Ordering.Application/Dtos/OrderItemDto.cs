namespace Ordering.Application.Dtos;

public record OrderItemDto(OrderId orderId, ProductId productId, int quantity, decimal price);
