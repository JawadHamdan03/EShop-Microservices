using Basket.API.Data;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.API.Baskets.DeleteBasket;


public sealed record DeleteBasketResult(bool isSuccess);

public sealed record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketValidator()
    {
        RuleFor(x=>x.UserName).NotEmpty().WithMessage("Username is required.");
    }
}

public class DeleteBasketHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
      var res = await  basketRepository.DeleteBasket(request.UserName, cancellationToken);
      return new DeleteBasketResult(res);
    }
}