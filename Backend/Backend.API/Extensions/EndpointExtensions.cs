using User.API.Endpoints;
using Vocabulary.API.Endpoints;

namespace Backend.API.Extensions;

public static class EndpointExtensions
{
    public static WebApplication MapApplicationEndpoints(this WebApplication app)
    {
        app.MapVocabularyEndpoints();
        app.MapUserEndpoints();
        app.MapAdminUserEndpoints();
        app.MapUserActivityEndpoints();
        app.MapAuthEndpoints();

        return app;
    }
}
