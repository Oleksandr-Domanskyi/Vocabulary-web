namespace User.Application.DTO.Requests.Auth;

public sealed record ConfirmEmailRequest(
    Guid UserId,
    string Token);
