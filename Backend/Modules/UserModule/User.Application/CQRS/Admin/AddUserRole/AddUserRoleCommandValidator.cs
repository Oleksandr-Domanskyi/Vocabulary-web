using Backend.Shared.Validation;
using FluentValidation;
using User.Application.Common.Validation;

namespace User.Application.CQRS.Admin.AddUserRole;

public sealed class AddUserRoleCommandValidator : AbstractValidator<AddUserRoleCommand>
{
    public AddUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();

        RuleFor(x => x.Data)
            .NotNull()
            .ChildRules(data =>
            {
                data.RuleFor(x => x.Role).Role();
            })
            .WithoutPropertyPrefix();
    }
}
