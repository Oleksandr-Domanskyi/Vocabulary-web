using Backend.Shared.Validation;
using FluentValidation;

namespace User.Application.CQRS.Auth.LogoutUser;

public sealed class LogoutUserCommandValidator : AbstractValidator<LogoutUserCommand>
{
    public LogoutUserCommandValidator()
    {
        RuleFor(x => x.Data)
            .NotNull()
            .ChildRules(data =>
            {
                data.RuleFor(x => x.RefreshToken).NotEmpty();
            })
            .WithoutPropertyPrefix();
    }
}
