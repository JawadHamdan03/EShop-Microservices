using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductById;

public class GetProductByIdEndpoint : ICarterModule
{
    public sealed record GetProductByIdResponse(string Name,List<string>Category,string Description,
        string ImageFile, decimal Price);
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (ISender sender,string id) =>
        {
            var res = await sender.Send(new GetProductByIdQuery(Guid.Parse(id)));
            var response = res.Product.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        });
    }
}