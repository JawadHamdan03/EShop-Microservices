using Carter;
using Catalog.API.Entities;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public sealed record GetProductResponse(IEnumerable<Product> Products);

    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",async (ISender sender) =>
        {
            var res = await sender.Send(new GetProdcutsQuery());
            var response = res.Adapt<GetProductResponse>();
            return Results.Ok(res);
        }).WithName("Get Products")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products")
            ;
    }
}