using Carter;
using Catalog.API.Entities;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public sealed record GetProductResponse(IEnumerable<Product> Products);
    public sealed record GetProductRequest(int? PageNumber=1 , int? PageSzie =10);

    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",async ([AsParameters]GetProductRequest request,ISender sender) =>
            { 
            var query = request.Adapt<GetProdcutsQuery>();
            var res = await sender.Send(query);
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