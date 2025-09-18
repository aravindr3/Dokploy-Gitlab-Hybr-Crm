using FluentValidation;
using HyBrForex.Domain.CurrencyBoard.DTO.Request;

namespace HyBrForex.Infrastructure.Persistence;

public class CurrencyBoardRequestValidator : AbstractValidator<CurrencyBoardRequest>
{
    public CurrencyBoardRequestValidator()
    {
        RuleFor(product => product.BuyValue)
            .NotEmpty().WithMessage("Buy margin rate is required.")
            .GreaterThan(0).WithMessage("Buy margin rate greater than 0.");

        RuleFor(product => product.SellValue)
            .NotEmpty().WithMessage("Sell margin rate is required.")
            .GreaterThan(0).WithMessage("Sell margin rate greater than 0.");
    }
}