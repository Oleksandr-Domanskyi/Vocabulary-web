using Vocabulary.Application.Common.Errors;

namespace Vocabulary.Application.Common.Exeptions;

public sealed class VocabularyException : Exception
{
    public Error? Error { get; }

    public VocabularyException()
    {
    }

    public VocabularyException(string message)
        : base(message)
    {
    }

    public VocabularyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public VocabularyException(Error error)
        : base(error.Message)
    {
        Error = error;
    }

    public VocabularyException(Error error, Exception innerException)
        : base(error.Message, innerException)
    {
        Error = error;
    }
}

