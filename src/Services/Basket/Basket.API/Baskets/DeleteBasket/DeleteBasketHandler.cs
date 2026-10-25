using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.API.Baskets.DeleteBasket;


public sealed record DeleteBasketResult(bool isSuccess);

public sealed record DeleteBasketCommand(string username) : ICommand<DeleteBasketResult>;

public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketValidator()
    {
        RuleFor(x=>x.username).NotEmpty().WithMessage("Username is required.");
    }
}

public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}