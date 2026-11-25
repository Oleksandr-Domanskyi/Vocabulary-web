using Backend.Shared.Validation;
using FluentValidation;
using User.Application.Common.Validation;

namespace User.Application.CQRS.Admin.RemoveUserClaim;

public sealed class RemoveUserClaimCommandValidator : AbstractValidator<RemoveUserClaimCommand>
{
    public RemoveUserClaimCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();

        RuleFor(x => x.Data)
            .NotNull()
            .ChildRules(data =>
            {
                data.RuleFor(x => x.Type).ManageableClaimType();
                data.RuleFor(x => x.Value).NotEmpty();
            })
            .WithoutPropertyPrefix();
    }
}
