using Basket.API.Entities;
using BuildingBlocks.CQRS;

namespace Basket.API.Baskets.GetBasket;


public sealed record GetBasketResult(ShoppingCart ShoppingCart);

public sealed record GetBasketQuery(string userName) : IQuery<GetBasketResult>;


public class GetBasketHandler : IQueryHandler<GetBasketQuery,GetBasketResult>
{
    public Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}