using Backend.Shared.Validation;
using FluentValidation;
using Vocabulary.Core.DTO.Request.Dictionary;
using Vocabulary.Core.DTO.Request.Transaltion;

namespace Vocabulary.Application.Common.Validation;

/// <summary>
/// Rules shared by the create and update validators.
/// Length limits and uniqueness mirror the EF configurations in Vocabulary.Infrastructure.
/// </summary>
public static class VocabularyRuleExtensions
{
    public static IRuleBuilderOptions<T, string> Word<T>(this IRuleBuilder<T, string> rule) =>
        rule.NotEmpty().MaximumLength(100);

    public static IRuleBuilderOptions<T, string> Definition<T>(this IRuleBuilder<T, string> rule) =>
        rule.NotEmpty().MaximumLength(2000);

    public static IRuleBuilderOptions<T, string> Transcription<T>(this IRuleBuilder<T, string> rule) =>
        rule.NotEmpty().MaximumLength(150);

    public static IRuleBuilderOptions<T, string?> ImageUrl<T>(this IRuleBuilder<T, string?> rule) =>
        rule.MaximumLength(2048).HttpUrl();

    /// <summary>
    /// Requires the name of a <typeparamref name="TEnum"/> member, ignoring case.
    /// </summary>
    public static IRuleBuilderOptions<T, string> EnumName<T, TEnum>(this IRuleBuilder<T, string> rule)
        where TEnum : struct, Enum =>
        rule.NotEmpty()
            .IsEnumName(typeof(TEnum), caseSensitive: false)
            .WithMessage($"'{{PropertyName}}' must be one of: {string.Join(", ", Enum.GetNames<TEnum>())}.");

    public static IRuleBuilderOptions<T, List<DictionaryRequest>> UniquePerLanguage<T>(
        this IRuleBuilder<T, List<DictionaryRequest>> rule) =>
        rule.Must(items => AreUnique(items, x => (x.Name, x.Language?.ToUpperInvariant())))
            .WithMessage("'{PropertyName}' must not contain the same dictionary twice for one language.");

    public static IRuleBuilderOptions<T, List<TranslationRequest>> UniquePerLanguage<T>(
        this IRuleBuilder<T, List<TranslationRequest>> rule) =>
        rule.Must(items => AreUnique(items, x => x.LanguageCode?.ToUpperInvariant()))
            .WithMessage("'{PropertyName}' must contain at most one translation per language.");

    /// <summary>
    /// Requires the selected IDs, when present, to be non-empty and not repeated in the list.
    /// </summary>
    public static IRuleBuilderOptions<T, List<TItem>> UniqueIds<T, TItem>(
        this IRuleBuilder<T, List<TItem>> rule, Func<TItem, Guid?> getId, string idName = "id")
        where TItem : class =>
        rule.Must(items => AreUnique(items?.Where(x => x is not null && getId(x) is not null), getId))
            .WithMessage($"'{{PropertyName}}' must not contain the same '{idName}' twice.")
            .Must(items => items is null || items.TrueForAll(x => x is null || getId(x) != Guid.Empty))
            .WithMessage($"'{{PropertyName}}' must not contain an empty '{idName}'.");

    // Null items are reported by their own NotNull rule.
    private static bool AreUnique<TItem, TKey>(IEnumerable<TItem>? items, Func<TItem, TKey> getKey)
        where TItem : class
    {
        var keys = (items ?? []).Where(x => x is not null).Select(getKey).ToList();
        return keys.Distinct().Count() == keys.Count;
    }
}
