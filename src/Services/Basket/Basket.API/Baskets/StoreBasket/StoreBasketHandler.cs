using Basket.API.Data;
using Basket.API.Entities;
using BuildingBlocks.CQRS;
using Discount.Grpc;
using FluentValidation;

namespace Basket.API.Baskets.StoreBasket;


public sealed record StoreBasketResult(string UserName);

public sealed record StoreBasketCommand(ShoppingCart ShoppingCart):ICommand<StoreBasketResult>;

public class StoreBasketHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    : ICommandHandler<StoreBasketCommand,StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.ShoppingCart.Items)
        {
            var coupon = await discountProtoServiceClient.
                GetDiscountAsync(new GetDiscountRequest(){ProductName = item.ProductName},cancellationToken:cancellationToken);
            item.Price -= coupon.Amount;
        }

        var res = await basketRepository.StoreBasket(request.ShoppingCart,cancellationToken);
        return new StoreBasketResult(request.ShoppingCart.UserName);
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