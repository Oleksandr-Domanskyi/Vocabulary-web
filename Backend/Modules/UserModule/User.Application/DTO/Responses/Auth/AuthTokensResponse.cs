namespace User.Application.DTO.Responses.Auth;

public sealed record AuthTokensResponse(
    string AccessToken,
    string RefreshToken,
    DateTimeOffset AccessTokenExpiresAt,
    string TokenType = "Bearer");
