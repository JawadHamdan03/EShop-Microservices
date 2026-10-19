

using BuildingBlocks.CQRS;

namespace Catalog.API.Products.CreateProduct;



public record CreateProductResult(Guid Id);

public sealed record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price ): ICommand<CreateProductResult>;