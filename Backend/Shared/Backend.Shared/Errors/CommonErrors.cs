
namespace Backend.Shared.Errors;

public static class CommonErrors
{
    private const string BaseCode = "Common";

    private const string NotFoundCode = "NotFound";
    private const string InvalidIdCode = "InvalidId";
    private const string DuplicateIdCode = "DuplicateId";
    private const string InvalidEnumCode = "InvalidEnum";
    private const string UnexpectedErrorCode = "UnexpectedError";

    public static Error NotFound(string entityName, Guid id) =>
        ErrorFactory.Create(BaseCode, NotFoundCode, $"{entityName} with ID '{id}' was not found.", ErrorType.NotFound);
    public static Error NotFound(string entityName, string value) =>
        ErrorFactory.Create(BaseCode, NotFoundCode, $"{entityName} with value '{value}' was not found.", ErrorType.NotFound);

    public static Error InvalidId() =>
        ErrorFactory.Create(BaseCode, InvalidIdCode, "The provided ID is invalid.", ErrorType.Validation);

    public static Error Duplicated(Guid id) =>
        ErrorFactory.Create(BaseCode, DuplicateIdCode, $"Duplicate entity ID '{id}' in the request.", ErrorType.Validation);

    public static Error InvalidEnum(string enumType, string value) =>
        ErrorFactory.Create(BaseCode, InvalidEnumCode, $"Invalid value '{value}' for enum '{enumType}'.", ErrorType.Validation);

    public static Error UnexpectedError =>
        ErrorFactory.Create(BaseCode, UnexpectedErrorCode, "An unexpected server error occurred.", ErrorType.Unexpected);
}
