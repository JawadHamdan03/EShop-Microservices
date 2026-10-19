using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQueryHanlder<in TQuery, TResponse> : IRequestHandler<TQuery, Unit>
    where TQuery: IQuery<Unit>
    where TResponse:notnull;


public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery,TResponse>
    where TQuery: IQuery<TResponse>
    where TResponse:notnull
{
    
}