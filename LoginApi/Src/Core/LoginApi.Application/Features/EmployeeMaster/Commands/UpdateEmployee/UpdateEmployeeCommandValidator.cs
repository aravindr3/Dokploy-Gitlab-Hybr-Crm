using FluentValidation;
using HyBrForex.Application.Interfaces;

namespace HyBrForex.Application.Features.EmployeeMaster.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator(ITranslator translator)
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(p => translator[nameof(p.Name)]);
    }
}