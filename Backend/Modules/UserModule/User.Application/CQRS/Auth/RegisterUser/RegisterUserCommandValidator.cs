using Backend.Shared.Validation;
using FluentValidation;
using User.Application.Common.Validation;

namespace User.Application.CQRS.Auth.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Data)
            .NotNull()
            .ChildRules(data =>
            {
                data.RuleFor(x => x.UserName).UserName();
                data.RuleFor(x => x.Email).Email();
                data.RuleFor(x => x.Password).NewPassword();
            })
            .WithoutPropertyPrefix();
    }
}
