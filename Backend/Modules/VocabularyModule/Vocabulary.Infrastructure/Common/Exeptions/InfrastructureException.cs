using Backend.Shared.Errors;
using Vocabulary.Infrastructure.Common.Errors;

namespace Vocabulary.Infrastructure.Common.Exeptions;

#pragma warning disable RCS1194
// An Error object is required for every infrastructure exception.

public sealed class InfrastructureException(Error error, Exception? innerException = null) : Exception(error.Message, innerException)
{
    public Error Error { get; } = error;
}

#pragma warning restore RCS119
