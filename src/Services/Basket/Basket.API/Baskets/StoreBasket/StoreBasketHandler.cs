using Basket.API.Entities;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.API.Baskets.StoreBasket;


public sealed record StoreBasketResult(string username);

public sealed record StoreBasketCommand(ShoppingCart ShoppingCart):ICommand<StoreBasketResult>;

public class StoreBasketHandler : ICommandHandler<StoreBasketCommand,StoreBasketResult>
{
    public Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Cart Can't be null.");
        RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("Username can't be empty.");
    }
}