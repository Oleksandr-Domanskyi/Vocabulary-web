namespace User.Application.DTO.Responses.Admin;

public sealed record PagedUsersResponse(
    IReadOnlyList<AdminUserResponse> Items,
    int Page,
    int PageSize,
    int TotalCount);
