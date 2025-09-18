using FluentValidation;
using HyBrForex.Application.Features.Currency.Commands.CreateCurrencyMaster;
using HyBrForex.Application.Interfaces;

namespace HyBrForex.Application.Features.Currency.Commands.UpdateCurrencyMaster;

public class UpdateCurrencyMasterCommandValidator : AbstractValidator<CreateCurrencyMasterCommand>
{
    public UpdateCurrencyMasterCommandValidator(ITranslator translator)
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