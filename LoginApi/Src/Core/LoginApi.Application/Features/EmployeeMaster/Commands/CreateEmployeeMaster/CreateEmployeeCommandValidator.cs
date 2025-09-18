using FluentValidation;
using HyBrForex.Application.Interfaces;

namespace HyBrForex.Application.Features.EmployeeMaster.Commands.CreateEmployeeMaster;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeMasterCommand>
{
    public CreateEmployeeCommandValidator(ITranslator translator)
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(p => translator[nameof(p.Name)]);
    }
}