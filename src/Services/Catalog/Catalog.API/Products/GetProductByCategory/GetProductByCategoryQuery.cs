using BuildingBlocks.CQRS;
using Catalog.API.Entities;
using Marten;

namespace Catalog.API.Products.GetProductByCategory;


public sealed record GetProductByCategoryQueryResult(IEnumerable<Product> product);

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryQueryResult>;

public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery,GetProductByCategoryQueryResult>
{
    public async Task<GetProductByCategoryQueryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {

        var res = await session.Query<Product>().Where(x => x.Category
                .Contains(query.Category))
                .ToListAsync(cancellationToken);

        return new GetProductByCategoryQueryResult(res);
    }
}