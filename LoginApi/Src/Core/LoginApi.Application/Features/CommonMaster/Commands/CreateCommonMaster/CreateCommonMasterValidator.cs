using FluentValidation;
using HyBrForex.Application.Interfaces;

namespace HyBrForex.Application.Features.CommonMaster.Commands.CreateCommonMaster;

public class CreateCommonMasterValidator : AbstractValidator<CreateCommonMasterCommand>
{
    public CreateCommonMasterValidator(ITranslator translator)
    {
        RuleFor(p => p.CommonName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(p => translator[nameof(p.CommonName)]);
    }
}