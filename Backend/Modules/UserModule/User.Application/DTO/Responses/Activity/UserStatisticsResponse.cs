namespace User.Application.DTO.Responses.Activity;

public sealed record UserStatisticsResponse(
    int Level,
    int ExperiencePoints,
    int TotalActivityCount,
    int TotalEarnedExperience,
    int CurrentStreak);
