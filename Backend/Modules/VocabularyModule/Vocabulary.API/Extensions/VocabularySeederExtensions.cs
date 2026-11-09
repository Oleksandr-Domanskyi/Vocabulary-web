using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Vocabulary.Infrastructure.Data;
using Vocabulary.Infrastructure.Seed;

namespace Vocabulary.API.Extensions;

public static class VocabularySeederExtensions
{
    public static async Task SeedUserVocabularyAsync(this WebApplication app, Guid? userId)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var context = scope.ServiceProvider
            .GetRequiredService<VocabularyDbContext>();

        await VocabularySeed.SeedUserVocabularyAsync(context, userId!.Value);
    }
}
