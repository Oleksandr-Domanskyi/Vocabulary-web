namespace User.Application.DTO.Responses.Users;

public sealed record UserClaimResponse(
    string Type,
    string Value);
