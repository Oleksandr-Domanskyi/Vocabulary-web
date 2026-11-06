namespace User.Application.DTO.Responses.Activity;

public sealed record UserActivityResponse(
    DateOnly Date,
    int ActivityCount,
    int EarnedExperience);
