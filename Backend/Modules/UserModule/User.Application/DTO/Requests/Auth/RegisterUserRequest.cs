namespace User.Application.DTO.Requests.Auth;

public sealed record RegisterUserRequest(string UserName, string Email, string Password);
