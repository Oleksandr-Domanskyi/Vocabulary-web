using Backend.Shared.Validation;
using FluentValidation;
using User.Application.Common.Validation;

namespace User.Application.CQRS.Users.UpdateCurrentUser;

public sealed class UpdateCurrentUserCommandValidator : AbstractValidator<UpdateCurrentUserCommand>
{
    public UpdateCurrentUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();

        RuleFor(x => x.Data)
            .NotNull()
            .ChildRules(data =>
            {
                data.RuleFor(x => x.UserName).UserName();
                data.RuleFor(x => x.AvatarUrl).MaximumLength(2048).HttpUrl();
            })
            .WithoutPropertyPrefix();
    }
}
