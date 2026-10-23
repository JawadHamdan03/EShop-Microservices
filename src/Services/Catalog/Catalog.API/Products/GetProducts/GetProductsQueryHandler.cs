using BuildingBlocks.CQRS;
using Catalog.API.Entities;
using Marten;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
    : IQueryHandler<GetProdcutsQuery,GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProdcutsQuery query, CancellationToken cancellationToken)
    {
        
        var res =  await session.Query<Product>()
            .ToPagedListAsync(query.PageNumber??1,query.PageSize??10,cancellationToken);
        
        return new GetProductsResult(products : res);

    }
}