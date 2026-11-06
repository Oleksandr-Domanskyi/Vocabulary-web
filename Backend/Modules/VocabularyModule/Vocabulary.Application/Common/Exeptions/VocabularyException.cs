using Backend.Shared.Errors;
using Vocabulary.Application.Common.Errors;

namespace Vocabulary.Application.Common.Exeptions;

// Standard exception constructors are intentionally omitted
// because VocabularyException requires an Error object.
#pragma warning disable RCS1194

public sealed class VocabularyException(Error error) : Exception(error.Message)
{
    public Error Error { get; } = error;
}

#pragma warning restore RCS1194
