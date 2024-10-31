namespace Basket.API.Basket.CreateBasket;

public record CreateBasketCommand(ShoppingCart Cart) : ICommand<CreateBasketResult>;

public record CreateBasketResult(string UserName);

public class BasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public BasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart is required");
        RuleFor(x => x.Cart.Items).NotNull().WithMessage("Items are required");
    }
}

public class CreateBasketHandle(
    IBasketRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProto
) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(
        CreateBasketCommand command,
        CancellationToken cancellationToken
    )
    {
        await DeductDiscount(command.Cart, cancellationToken);
        await repository.CreateBasket(command.Cart, cancellationToken);
        return new CreateBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(
                new GetDiscountRequest { ProductName = item.ProductName },
                cancellationToken: cancellationToken
            );
            item.Price -= coupon.Amount;
        }
    }
}
