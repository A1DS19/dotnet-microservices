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

public class CreateBasketHandle(IBasketRepository repository)
    : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(
        CreateBasketCommand command,
        CancellationToken cancellationToken
    )
    {
        ShoppingCart cart = command.Cart;
        await repository.CreateBasket(cart, cancellationToken);
        return new CreateBasketResult(cart.UserName);
    }
}
