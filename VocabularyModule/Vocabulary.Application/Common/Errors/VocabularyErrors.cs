using Vocabulary.Application.Common.Errors;

namespace Vocabulary.Application.Common.Errors;

public static class VocabularyErrors
{
    public static Error NotFound(Guid id) =>
        new("Vocabulary.NotFound", $"Vocabulary with ID '{id}' was not found.", ErrorType.NotFound);

    public static Error AlreadyExists(string word) =>
        new("Vocabulary.AlreadyExists", $"Vocabulary word '{word}' already exists.", ErrorType.Conflict);

    public static Error InvalidWord =>
        new("Vocabulary.InvalidWord", "The vocabulary word is invalid.", ErrorType.Validation);

    public static Error InvalidEnum(string enumType, string value) =>
    new("Vocabulary.InvalidEnum", $"Invalid value '{value}' for enum '{enumType}'.", ErrorType.Validation);

    public static Error InvalidExampleIndex(int index, int count) =>
        new("Vocabulary.InvalidExampleIndex", $"Example index '{index}' is out of range. Available examples: {count}.", ErrorType.Validation);
}
