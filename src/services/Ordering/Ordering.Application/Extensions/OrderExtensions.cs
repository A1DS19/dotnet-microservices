namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(o => new OrderDto(
            o.Id.Value,
            o.CustomerId.Value,
            o.OrderName.Value,
            ShippingAddress: new AddressDto(
                o.ShippingAddress.Street,
                o.ShippingAddress.City,
                o.ShippingAddress.State,
                o.ShippingAddress.Country,
                o.ShippingAddress.ZipCode
            ),
            BillingAddress: new AddressDto(
                o.BillingAddress.Street,
                o.BillingAddress.City,
                o.BillingAddress.State,
                o.BillingAddress.Country,
                o.BillingAddress.ZipCode
            ),
            Payment: new PaymentDto(
                o.Payment.CardNumber,
                o.Payment.CardHolderName,
                o.Payment.Expiration,
                o.Payment.SecurityNumber
            ),
            o.OrderStatus,
            OrderItems: o.OrderItems.Select(oi => new OrderItemDto(
                    oi.Id.Value,
                    oi.ProductId.Value,
                    oi.Quantity,
                    oi.Price
                ))
                .ToList()
        ));
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    private static OrderDto DtoFromOrder(Order order)
    {
        return new OrderDto(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName.Value,
            ShippingAddress: new AddressDto(
                order.ShippingAddress.Street,
                order.ShippingAddress.City,
                order.ShippingAddress.State,
                order.ShippingAddress.Country,
                order.ShippingAddress.ZipCode
            ),
            BillingAddress: new AddressDto(
                order.BillingAddress.Street,
                order.BillingAddress.City,
                order.BillingAddress.State,
                order.BillingAddress.Country,
                order.BillingAddress.ZipCode
            ),
            Payment: new PaymentDto(
                order.Payment.CardNumber,
                order.Payment.CardHolderName,
                order.Payment.Expiration,
                order.Payment.SecurityNumber
            ),
            OrderStatus: order.OrderStatus,
            OrderItems: order
                .OrderItems.Select(oi => new OrderItemDto(
                    oi.OrderId.Value,
                    oi.ProductId.Value,
                    oi.Quantity,
                    oi.Price
                ))
                .ToList()
        );
    }
}
