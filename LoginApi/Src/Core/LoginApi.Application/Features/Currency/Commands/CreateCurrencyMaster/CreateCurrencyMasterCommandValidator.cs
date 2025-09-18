using FluentValidation;
using HyBrForex.Application.Interfaces;

namespace HyBrForex.Application.Features.Currency.Commands.CreateCurrencyMaster;

public class CreateCurrencyMasterCommandValidator : AbstractValidator<CreateCurrencyMasterCommand>
{
    public CreateCurrencyMasterCommandValidator(ITranslator translator)
    {
        RuleFor(p => p.CountryId)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(p => translator[nameof(p.CountryId)]);

        RuleFor(c => c.ISD)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(c => translator[nameof(c.ISD)]);

        RuleFor(x => x.Currency)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(x => translator[nameof(x.Currency)]);
    }
}