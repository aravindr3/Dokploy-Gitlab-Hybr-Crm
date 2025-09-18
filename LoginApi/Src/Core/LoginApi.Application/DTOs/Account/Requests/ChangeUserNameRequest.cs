using FluentValidation;
using HyBrForex.Application.Helpers;
using HyBrForex.Application.Interfaces;

namespace HyBrForex.Application.DTOs.Account.Requests;

public class ChangeEmailRequest
{
    public string Email { get; set; }
}

public class ChangeEmailRequestValidator : AbstractValidator<ChangeEmailRequest>
{
    public ChangeEmailRequestValidator(ITranslator translator)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .MinimumLength(4)
            .Matches(Regexs.UserName)
            .WithName(p => translator[nameof(p.Email)]);
    }
}