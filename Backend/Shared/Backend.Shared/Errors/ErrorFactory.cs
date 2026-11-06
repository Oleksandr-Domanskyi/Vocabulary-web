namespace Backend.Shared.Errors;

/// <summary>
/// Creates standardized errors with a consistent code format.
/// </summary>
public static class ErrorFactory
{
    public static Error Create(string source, string code, string message, ErrorType type)
        => new($"{source}.{code}", message, type);
}
