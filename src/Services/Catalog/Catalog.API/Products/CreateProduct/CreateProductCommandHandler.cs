
using BuildingBlocks.CQRS;
using Catalog.API.Entities;
using Marten;

namespace Catalog.API.Products.CreateProduct;

public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand,CreateProductResult>
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
        

        session.Store(prod);
        await session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(prod.Id);
    }
}