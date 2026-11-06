using Backend.Shared.Database;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entity.Translation;
using Vocabulary.Core.Entity.Vocabulary;
using Vocabulary.Infrastructure.Data.Configurations;

namespace Vocabulary.Infrastructure.Data;

public class VocabularyDbContext(DbContextOptions<VocabularyDbContext> options) : GlobalVocabularyDbContext(options)
{
    public override string Schema => "Vocabulary";

    public DbSet<VocabularyModel> VocabularyModels { get; set; } = default!;
    public DbSet<Example> Examples { get; set; } = default!;
    public DbSet<Dictionary> Dictionaries { get; set; } = default!;
    public DbSet<TranslationModel> TranslationModels { get; set; } = default!;
    public DbSet<TranslationExample> TranslationExamples { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VocabularyModelConfiguration).Assembly);
    }

}

