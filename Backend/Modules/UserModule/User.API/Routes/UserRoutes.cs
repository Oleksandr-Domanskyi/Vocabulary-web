namespace User.API.Routes;

public static class UserRoutes
{
    public const string Base = "/api/users";
    public const string Me = "/me";
    public const string UpdateMe = "/me";
    public const string DeleteMe = "/me";
    public const string ById = "/{id:guid}";
    public const string MyRoles = "/me/roles";
    public const string MyClaims = "/me/claims";

    public const string AdminBase = "/api/admin/users";
    public const string AdminAll = "/";
    public const string AdminRoles = "/{id:guid}/roles";
    public const string AdminRole = "/{id:guid}/roles/{role}";
    public const string AdminClaims = "/{id:guid}/claims";
    public const string AdminRevokeSessions = "/{id:guid}/revoke-sessions";
}
