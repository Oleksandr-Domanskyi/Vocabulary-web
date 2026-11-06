
using Backend.Shared.Errors;

namespace Vocabulary.Infrastructure.Common.Errors;

public static class InfrastructureErrors
{
    private const string BaseCode = "Vocabulary.Infrastructure";

    private const string UnexpectedCode = "Unexpected";
    private const string DatabaseFailureCode = "DatabaseFailure";
    private const string DatabaseConnectionCode = "DatabaseConnection";
    private const string StorageFailureCode = "StorageFailure";
    private const string ExamplesCountMismatchCode = "ExamplesCountMismatch";

    public static Error Unexpected =>
        ErrorFactory.Create(BaseCode, UnexpectedCode, "An unexpected infrastructure error occurred.", ErrorType.Unexpected);

    public static Error DatabaseFailure =>
        ErrorFactory.Create(BaseCode, DatabaseFailureCode, "A database operation failed.", ErrorType.Unexpected);

    public static Error DatabaseConnection =>
        ErrorFactory.Create(BaseCode, DatabaseConnectionCode, "Could not connect to the database.", ErrorType.Unexpected);

    public static Error StorageFailure =>
        ErrorFactory.Create(BaseCode, StorageFailureCode, "A file storage operation failed.", ErrorType.Unexpected);

    public static Error ExamplesCountMismatch(int expected, int actual) =>
        ErrorFactory.Create(BaseCode, ExamplesCountMismatchCode, $"Expected {expected} examples, but received {actual}.", ErrorType.Unexpected);
}
