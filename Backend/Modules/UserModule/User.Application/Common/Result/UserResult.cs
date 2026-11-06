using User.Application.DTO.Responses.Users;

namespace User.Application.Common.Result;

public sealed record UserError(string Code, string Message);

public sealed record UserResult<T>(T? Value, IReadOnlyList<UserError> Errors)
{
    public bool IsSuccess => Errors.Count == 0;

    public static UserResult<T> Ok(T value) => new(value, []);

    public static UserResult<T> Fail(string code, string message) =>
        new(default, [new UserError(code, message)]);

    public static UserResult<T> Fail(IReadOnlyList<UserError> errors) =>
        new(default, errors);
}
