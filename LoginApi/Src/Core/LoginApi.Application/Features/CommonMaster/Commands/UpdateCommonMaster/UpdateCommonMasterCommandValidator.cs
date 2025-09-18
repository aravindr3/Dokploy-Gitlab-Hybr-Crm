using FluentValidation;
using HyBrForex.Application.Interfaces;

namespace HyBrForex.Application.Features.CommonMaster.Commands.UpdateCommonMaster;

public class UpdateCommonMasterCommandValidator : AbstractValidator<UpdateCommonMasterCommand>
{
    public UpdateCommonMasterCommandValidator(ITranslator translator)
    {
        RuleFor(p => p.CommonName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(p => translator[nameof(p.CommonName)]);
    }
}