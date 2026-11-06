using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Vocabulary.Infrastructure.Data;
using Vocabulary.Infrastructure.Seed;

namespace Vocabulary.API.Extensions;

public static class VocabularySeederExtensions
{
    public static async Task SeedVocabularyAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<VocabularyDbContext>();

        await VocabularySeed.SeedAsync(context);
    }
}