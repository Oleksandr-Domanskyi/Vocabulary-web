using FluentValidation;

namespace User.Application.CQRS.Activity.GetCurrentUserStatistics;

public sealed class GetCurrentUserStatisticsQueryValidator : AbstractValidator<GetCurrentUserStatisticsQuery>
{
    public GetCurrentUserStatisticsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
