namespace User.Application.DTO.Requests.Users;

public sealed record UpdateUserProfileRequest(
    string UserName,
    string? AvatarUrl);
