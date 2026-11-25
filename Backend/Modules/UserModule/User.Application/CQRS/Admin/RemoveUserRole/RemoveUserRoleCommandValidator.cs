using FluentValidation;
using User.Application.Common.Validation;

namespace User.Application.CQRS.Admin.RemoveUserRole;

public sealed class RemoveUserRoleCommandValidator : AbstractValidator<RemoveUserRoleCommand>
{
    public RemoveUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Role).Role();
    }
}
