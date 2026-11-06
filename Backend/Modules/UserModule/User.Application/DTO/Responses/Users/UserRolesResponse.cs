namespace User.Application.DTO.Responses.Users;

public sealed record UserRolesResponse(IReadOnlyList<string> Roles);
