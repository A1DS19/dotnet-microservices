using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(CheckoutBasketDto BasketDto) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(x => x.BasketDto).NotNull().WithMessage("Basket cannot be null.");
        RuleFor(x => x.BasketDto.Username).NotNull().WithMessage("Username cannot be null.");
    }
}

public class CheckoutBasketCommandHandler(
    IBasketRepository basketRepository,
    IPublishEndpoint publishEndpoint
) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(
        CheckoutBasketCommand command,
        CancellationToken cancellationToken
    )
    {
        var basket = await basketRepository.GetBasket(
            command.BasketDto.Username,
            cancellationToken
        );

        if (basket is null)
        {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = command.BasketDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await basketRepository.DeleteBasket(command.BasketDto.Username, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
