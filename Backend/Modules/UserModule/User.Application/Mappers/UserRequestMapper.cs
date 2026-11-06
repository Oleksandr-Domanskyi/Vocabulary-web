using User.Application.DTO.Requests.Auth;
using User.Core.Entity;

namespace User.Application.Mappers;

public static class UserRequestMapper
{
    public static UserModel ToModel(RegisterUserRequest request) => new()
    {
        UserName = request.UserName,
        Email = request.Email
    };
}
