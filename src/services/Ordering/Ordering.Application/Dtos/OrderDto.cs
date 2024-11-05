namespace Ordering.Application.Dtos;

public record OrderDto(
    OrderId Id,
    CustomerId CustomerId,
    OrderName OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus OrderStatus,
    List<OrderItemDto> OrderItems
);
