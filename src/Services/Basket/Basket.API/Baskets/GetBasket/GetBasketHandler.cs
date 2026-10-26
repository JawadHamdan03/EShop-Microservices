using Basket.API.Data;
using Basket.API.Entities;
using BuildingBlocks.CQRS;
using Mapster;

namespace Basket.API.Baskets.GetBasket;


public sealed record GetBasketResult(ShoppingCart ShoppingCart);

public sealed record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;


public class GetBasketHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery,GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var res = await basketRepository.GetBasket(request.UserName);
        return new GetBasketResult(res);
    }
}