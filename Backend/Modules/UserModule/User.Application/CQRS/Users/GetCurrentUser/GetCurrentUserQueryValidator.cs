using FluentValidation;

namespace User.Application.CQRS.Users.GetCurrentUser;

public sealed class GetCurrentUserQueryValidator : AbstractValidator<GetCurrentUserQuery>
{
    public GetCurrentUserQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
