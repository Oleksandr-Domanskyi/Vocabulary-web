using FluentValidation;

namespace User.Application.CQRS.Activity.GetCurrentUserActivity;

public sealed class GetCurrentUserActivityQueryValidator : AbstractValidator<GetCurrentUserActivityQuery>
{
    public GetCurrentUserActivityQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
