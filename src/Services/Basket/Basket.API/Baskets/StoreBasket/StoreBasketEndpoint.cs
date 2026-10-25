using Basket.API.Entities;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Baskets.StoreBasket;


public sealed record StoreBasketRequest(ShoppingCart ShoppingCart);
public sealed record StoreBasketResponse(string username);


public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket",async (StoreBasketRequest request, ISender sender) =>
        {
            var res = await sender.Send(new StoreBasketCommand(request.ShoppingCart));

            var response =res.Adapt<StoreBasketResponse>();
            return Results.Created($"/basket/{response.username}",response);
        });
    }
}