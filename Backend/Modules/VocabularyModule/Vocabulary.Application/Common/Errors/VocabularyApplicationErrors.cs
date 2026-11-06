using Backend.Shared.Errors;

namespace Vocabulary.Application.Common.Errors;

public sealed class VocabularyApplicationErrors
{
    private const string BaseCode = "Vocabulary.Application";

    private const string AlreadyExistsCode = "AlreadyExists";
    private const string InvalidWordCode = "InvalidWord";

    public static Error AlreadyExists(string word) =>
        ErrorFactory.Create(BaseCode, AlreadyExistsCode, $"Vocabulary word '{word}' already exists.", ErrorType.Conflict);

    public static Error InvalidWord =>
        ErrorFactory.Create(BaseCode, InvalidWordCode, "The vocabulary word is invalid.", ErrorType.Validation);
}