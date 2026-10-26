using Basket.API.Entities;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Baskets.StoreBasket;


public sealed record StoreBasketRequest(ShoppingCart Cart);
public sealed record StoreBasketResponse(string UserName);


public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket",async (StoreBasketRequest request, ISender sender) =>
        {
            var res = await sender.Send(new StoreBasketCommand(request.Cart));

            var response =res.Adapt<StoreBasketResponse>();
            return Results.Created($"/basket/{response.UserName}",response);
        });
    }
}