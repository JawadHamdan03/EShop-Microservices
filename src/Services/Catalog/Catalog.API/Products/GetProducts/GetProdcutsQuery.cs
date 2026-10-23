using BuildingBlocks.CQRS;
using Catalog.API.Entities;

namespace Catalog.API.Products.GetProducts;

public sealed record GetProductsResult(IEnumerable<Product> products);

public sealed record GetProdcutsQuery(int? PageNumber=1 , int? PageSize=10): IQuery<GetProductsResult>;