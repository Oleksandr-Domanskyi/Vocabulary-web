using Backend.Shared.Validation;
using FluentValidation;
using User.Application.Common.Validation;
using User.Core.Constants;

namespace User.Application.CQRS.Admin.AddUserClaim;

public sealed class AddUserClaimCommandValidator : AbstractValidator<AddUserClaimCommand>
{
    public AddUserClaimCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();

        RuleFor(x => x.Data)
            .NotNull()
            .ChildRules(data =>
            {
                data.RuleFor(x => x.Type).ManageableClaimType();
                data.RuleFor(x => x.Value)
                    .NotEmpty()
                    .Must(value => value is SubscriptionTypes.Default or SubscriptionTypes.Pro or SubscriptionTypes.Business)
                    .WithMessage($"'{{PropertyName}}' must be one of: {SubscriptionTypes.Default}, {SubscriptionTypes.Pro}, {SubscriptionTypes.Business}.");
            })
            .WithoutPropertyPrefix();
    }
}
