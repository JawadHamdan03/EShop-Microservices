using Basket.API.Entities;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Baskets.GetBasket;


public sealed record GetBasketRequest(string UserName);
public sealed record GetBasketResponse(ShoppingCart ShoppingCart);


public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{UserName}", async ([FromRoute]string UserName,ISender sender) =>
        {
            var res = await sender.Send(new GetBasketQuery(UserName));
            var response = res.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        }).WithName("GetProductById")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}