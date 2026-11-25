using FluentValidation;
using User.Core.Constants;

namespace User.Application.Common.Validation;

/// <summary>
/// Rules shared by several user validators.
/// User name and password rules mirror the options in UserIdentityExtension.
/// </summary>
public static class UserRuleExtensions
{
    // Length of the UserName and Email columns created by Identity.
    private const int IdentityMaxLength = 256;

    public static IRuleBuilderOptions<T, string> UserName<T>(this IRuleBuilder<T, string> rule) =>
        rule.NotEmpty()
            .MaximumLength(IdentityMaxLength)
            .Matches(@"^[a-zA-Z0-9\-._@+]*\z")
            .WithMessage("'{PropertyName}' can only contain Latin letters, digits and the characters - . _ @ +");

    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> rule) =>
        rule.NotEmpty()
            .MaximumLength(IdentityMaxLength)
            .EmailAddress();

    public static IRuleBuilderOptions<T, string> NewPassword<T>(this IRuleBuilder<T, string> rule) =>
        rule.NotEmpty()
            .MinimumLength(8)
            .MaximumLength(128)
            .Matches("[0-9]").WithMessage("'{PropertyName}' must contain at least one digit.")
            .Matches("[a-z]").WithMessage("'{PropertyName}' must contain at least one lowercase letter.")
            .Matches("[A-Z]").WithMessage("'{PropertyName}' must contain at least one uppercase letter.")
            .Matches("[^a-zA-Z0-9]").WithMessage("'{PropertyName}' must contain at least one special character.")
            .Must(password => password is null || password.Distinct().Count() >= 4)
            .WithMessage("'{PropertyName}' must contain at least 4 different characters.");

    public static IRuleBuilderOptions<T, string> Role<T>(this IRuleBuilder<T, string> rule) =>
        rule.NotEmpty()
            .Must(role => role is UserRoles.User or UserRoles.Admin)
            .WithMessage($"'{{PropertyName}}' must be one of: {UserRoles.User}.");

    public static IRuleBuilderOptions<T, string> ManageableClaimType<T>(this IRuleBuilder<T, string> rule) =>
        rule.NotEmpty()
            .Must(type => type == UserClaims.Subscription)
            .WithMessage($"'{{PropertyName}}' must be '{UserClaims.Subscription}'.");
}
