namespace User.Application.DTO.Responses.Auth;

public sealed record RegisterUserResponse(
    Guid UserId,
    string UserName,
    string Email,
    bool RequiresEmailConfirmation);
