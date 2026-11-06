using User.Core.Entity;
using User.Application.DTO.Responses.Users;
using User.Application.DTO.Responses.Auth;

namespace User.Application.Mappers;

public static class UserResponseMapper
{
    public static UserProfileResponse ToProfile(UserModel user) => new(
        user.Id,
        user.UserName ?? string.Empty,
        user.Email ?? string.Empty,
        user.EmailConfirmed,
        user.AvatarUrl,
        user.Level,
        user.ExperiencePoints,
        user.CreatedAt,
        user.LastActiveAt);

    public static PublicUserProfileResponse ToPublicProfile(UserModel user) => new(
        user.Id,
        user.UserName ?? string.Empty,
        user.AvatarUrl,
        user.Level);

    public static RegisterUserResponse ToRegisterResponse(UserModel user) => new(
        user.Id,
        user.UserName ?? string.Empty,
        user.Email ?? string.Empty,
        RequiresEmailConfirmation: !user.EmailConfirmed);
}
