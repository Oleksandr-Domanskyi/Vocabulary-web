namespace User.Application.DTO.Requests.Admin;

public sealed record GetUsersRequest(string? SearchTerm = null, int Page = 1, int PageSize = 20);
