using FluentValidation;

namespace User.Application.CQRS.Users.GetCurrentUserClaims;

public sealed class GetCurrentUserClaimsQueryValidator : AbstractValidator<GetCurrentUserClaimsQuery>
{
    public GetCurrentUserClaimsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
