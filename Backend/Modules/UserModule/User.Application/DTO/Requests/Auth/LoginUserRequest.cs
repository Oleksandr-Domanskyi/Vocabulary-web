namespace User.Application.DTO.Requests.Auth;

public sealed record LoginUserRequest(
    string Email,
    string Password);
