namespace User.Application.DTO.Requests.Auth;

public sealed record ResetPasswordRequest(
    string Email,
    string Token,
    string NewPassword);
