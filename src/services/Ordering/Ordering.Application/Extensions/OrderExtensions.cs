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
}
