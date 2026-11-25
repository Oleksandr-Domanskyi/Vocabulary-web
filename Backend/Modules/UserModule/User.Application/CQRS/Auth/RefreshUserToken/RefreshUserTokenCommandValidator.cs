using Backend.Shared.Validation;
using FluentValidation;

namespace User.Application.CQRS.Auth.RefreshUserToken;

public sealed class RefreshUserTokenCommandValidator : AbstractValidator<RefreshUserTokenCommand>
{
    public RefreshUserTokenCommandValidator()
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
