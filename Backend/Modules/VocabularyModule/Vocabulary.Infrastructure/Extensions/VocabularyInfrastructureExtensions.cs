using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Vocabulary.Infrastructure.Data;
using Vocabulary.Infrastructure.Repository;
using Backend.Shared.Extensions;

namespace Vocabulary.Infrastructure.Extensions;

public static class VocabularyInfrastructureExtensions
{
    public static void AddVocabularyInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase<VocabularyDbContext>(configuration);

        services.AddScoped<IVocabularyRepository, VocabularyRepository>();
    }
}
