using BuildingBlocks.CQRS;
using Catalog.API.Entities;
using Marten;

namespace Catalog.API.Products.GetProducts;

public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
    : IQueryHandler<GetProdcutsQuery,GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProdcutsQuery query, CancellationToken cancellationToken)
    {
        
        var res =  await session.Query<Product>().ToListAsync(cancellationToken);
        
        return new GetProductsResult(products : res);

    }
}