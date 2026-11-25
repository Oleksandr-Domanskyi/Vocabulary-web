using FluentValidation;

namespace User.Application.CQRS.Activity.GetCurrentUserActivityByDate;

public sealed class GetCurrentUserActivityByDateQueryValidator : AbstractValidator<GetCurrentUserActivityByDateQuery>
{
    public GetCurrentUserActivityByDateQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();
    }
}
