using Carter;
using Catalog.API.Entities;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductByCategory;

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public sealed  record GetProductsByCategoryResponse(List<Product> Products);
    
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var res = await sender.Send(new GetProductByCategoryQuery(category));
            var response = res.Adapt<GetProductsByCategoryResponse>();

            return Results.Ok(response);
        });
    }
}