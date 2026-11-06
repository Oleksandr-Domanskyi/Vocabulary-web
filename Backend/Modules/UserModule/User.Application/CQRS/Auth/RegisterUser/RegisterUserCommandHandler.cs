using MediatR;
using User.Application.DTO.Responses.Auth;
using User.Core.Entity;
using User.Application.Contracts;
using User.Application.Common.Result;
using User.Application.Mappers;
namespace User.Application.CQRS.Auth.RegisterUser;

public sealed class RegisterUserCommandHandler(IUserAccountService service) : IRequestHandler<RegisterUserCommand, UserResult<RegisterUserResponse>>
{
    public async Task<UserResult<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = UserRequestMapper.ToModel(request.Data);
        var result = await service.CreateAsync(user, request.Data.Password);

        if (!result.Succeeded)
            return UserResult<RegisterUserResponse>.Fail(result.Errors);

        return UserResult<RegisterUserResponse>.Ok(UserResponseMapper.ToRegisterResponse(user));

    }
}
