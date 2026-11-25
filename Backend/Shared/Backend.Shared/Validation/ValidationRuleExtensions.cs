using FluentValidation;

namespace Backend.Shared.Validation;

public static class ValidationRuleExtensions
{
    /// <summary>
    /// Requires an absolute http(s) URL. Empty values are left to NotEmpty.
    /// </summary>
    public static IRuleBuilderOptions<T, string?> HttpUrl<T>(this IRuleBuilder<T, string?> rule) =>
        rule.Must(value =>
                string.IsNullOrEmpty(value) ||
                (Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
                 (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)))
            .WithMessage("'{PropertyName}' must be an absolute http or https URL.");

    /// <summary>
    /// Removes the name of the wrapping property (e.g. "Data") from error keys,
    /// so that they match the JSON body sent by the client.
    /// </summary>
    public static IRuleBuilderOptions<T, TProperty> WithoutPropertyPrefix<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule) =>
        rule.OverridePropertyName(string.Empty);
}
