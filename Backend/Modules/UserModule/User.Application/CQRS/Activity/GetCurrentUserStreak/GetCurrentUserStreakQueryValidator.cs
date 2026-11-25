using FluentValidation;

namespace User.Application.CQRS.Activity.GetCurrentUserStreak;

public sealed class GetCurrentUserStreakQueryValidator : AbstractValidator<GetCurrentUserStreakQuery>
{
    public GetCurrentUserStreakQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
