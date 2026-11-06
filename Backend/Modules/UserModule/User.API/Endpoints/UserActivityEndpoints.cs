using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.API.Routes;

namespace User.API.Endpoints;

public static class UserActivityEndpoints
{
    public static void MapUserActivityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(UserActivityRoutes.Base)
            .WithTags("User Activity")
            .RequireAuthorization();

        group.MapGet(UserActivityRoutes.Activity, NotImplemented)
            .WithName("GetCurrentUserActivity")
            .WithSummary("Get current user activity")
            .WithDescription(
                "Retrieves the authenticated user's daily activity history, " +
                "including activity counts and earned experience points. " +
                "Results can be used to build an activity calendar.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet(UserActivityRoutes.ActivityByDate, NotImplemented)
            .WithName("GetCurrentUserActivityByDate")
            .WithSummary("Get user activity by date")
            .WithDescription(
                "Retrieves the authenticated user's activity for a specific date, " +
                "including the number of completed actions and experience points earned. " +
                "The date must be provided in YYYY-MM-DD format.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet(UserActivityRoutes.Streak, NotImplemented)
            .WithName("GetCurrentUserStreak")
            .WithSummary("Get current user streak")
            .WithDescription(
                "Calculates the authenticated user's current streak of consecutive " +
                "active days based on recorded daily activities. " +
                "A streak remains active if the user was active today or yesterday.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet(UserActivityRoutes.Statistics, NotImplemented)
            .WithName("GetCurrentUserStatistics")
            .WithSummary("Get current user statistics")
            .WithDescription(
                "Retrieves the authenticated user's learning statistics, " +
                "including total experience points, current level, " +
                "activity count, and learning progress.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    // Activity and XP changes should be triggered by trusted application actions.
    private static IResult NotImplemented() =>
        Results.Problem(
            statusCode: StatusCodes.Status501NotImplemented,
            title: "Endpoint not implemented");
}
