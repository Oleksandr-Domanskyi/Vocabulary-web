namespace User.Application.DTO.Responses.Activity;

public sealed record UserActivityHistoryResponse(
    IReadOnlyList<UserActivityResponse> Days);
