using BuildingBlocks.CQRS;
using Catalog.API.Entities;

namespace Catalog.API.Products.GetProducts;

public sealed record GetProductsResult(IEnumerable<Product> products);

public sealed record GetProdcutsQuery(): IQuery<GetProductsResult>;