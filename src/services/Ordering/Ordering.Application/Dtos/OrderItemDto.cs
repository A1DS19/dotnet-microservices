namespace Ordering.Application.Dtos;

public record OrderItemDto(Guid orderId, Guid productId, int quantity, decimal price);
