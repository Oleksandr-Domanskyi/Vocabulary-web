using Backend.Shared.Validation;
using FluentValidation;
using User.Application.Common.Validation;

namespace User.Application.CQRS.Auth.LoginUser;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Data)
            .NotNull()
            .ChildRules(data =>
            {
                data.RuleFor(x => x.Email).Email();
                data.RuleFor(x => x.Password).NotEmpty();
            })
            .WithoutPropertyPrefix();
    }
}
