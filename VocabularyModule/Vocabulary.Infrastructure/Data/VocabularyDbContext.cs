using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Data.Configurations;

namespace Vocabulary.Infrastructure.Data;

public class VocabularyDbContext(DbContextOptions<VocabularyDbContext> options) : DbContext(options)
{
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

