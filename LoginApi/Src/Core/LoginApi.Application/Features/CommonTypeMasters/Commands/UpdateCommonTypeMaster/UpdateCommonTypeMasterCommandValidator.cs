using FluentValidation;
using HyBrForex.Application.Interfaces;

namespace HyBrForex.Application.Features.CommonTypeMasters.Commands.UpdateCommonTypeMaster;

public class UpdateCommonTypeMasterCommandValidator : AbstractValidator<UpdateCommonTypeMasterCommand>
{
    public UpdateCommonTypeMasterCommandValidator(ITranslator translator)
    {
        RuleFor(c => c.TypeName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(c => translator[nameof(c.TypeName)]);
    }
}