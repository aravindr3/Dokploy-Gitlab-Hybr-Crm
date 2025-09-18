using FluentValidation;
using HyBrForex.Application.Interfaces;

namespace HyBrForex.Application.Features.CommonTypeMasters.Commands.CreateCommonTypeMaster;

public class CreateCommonTypeMasterCommandValidator : AbstractValidator<CreateCommonTypeMasterCommand>
{
    public CreateCommonTypeMasterCommandValidator(ITranslator translator)
    {
        RuleFor(p => p.TypeName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(p => translator[nameof(p.TypeName)]);
    }
}