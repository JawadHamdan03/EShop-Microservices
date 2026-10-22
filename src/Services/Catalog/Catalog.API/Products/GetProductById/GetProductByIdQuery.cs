using BuildingBlocks.CQRS;
using Catalog.API.Entities;
using Catalog.API.Exceptions;
using Marten;

namespace Catalog.API.Products.GetProductById;


public sealed record GetProductByIdQueryResult(Product Product);

public sealed record GetProductByIdQuery(Guid Id): IQuery<GetProductByIdQueryResult>;

public class GetProductByIdQueryHandler(IDocumentSession session,ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var prod = await session.LoadAsync<Product>(query.Id,cancellationToken);

        if (prod is null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        
        return new GetProductByIdQueryResult(prod);
    }
}