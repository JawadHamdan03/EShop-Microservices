
using BuildingBlocks.CQRS;
using Catalog.API.Entities;

namespace Catalog.API.Products.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand,CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var prod = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            ImageFile = request.ImageFile,
            Category = request.Category,
            Description = request.Description
        };
        
        // add and save to database 

        return new CreateProductResult(prod.Id);
    }
}