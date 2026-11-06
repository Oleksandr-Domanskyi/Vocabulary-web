using Backend.Shared.Errors;

namespace Backend.Shared.Errors;

/// <summary>
/// Represents an application error
/// </summary>
public sealed record Error(string Code, string Message, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Unexpected);
}