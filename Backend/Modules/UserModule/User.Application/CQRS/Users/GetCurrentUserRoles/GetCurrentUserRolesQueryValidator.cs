using FluentValidation;

namespace User.Application.CQRS.Users.GetCurrentUserRoles;

public sealed class GetCurrentUserRolesQueryValidator : AbstractValidator<GetCurrentUserRolesQuery>
{
    public GetCurrentUserRolesQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
