
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vocabulary.Core.Entity.Translation;
using Vocabulary.Core.Entity.Vocabulary;

namespace Vocabulary.Infrastructure.Data.Configurations;

public class TranslationExampleConfiguration : IEntityTypeConfiguration<TranslationExample>
{
    public void Configure(EntityTypeBuilder<TranslationExample> builder)
    {
        builder.ToTable("TranslationExamples");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ExampleId)
            .IsRequired();

        builder.Property(x => x.TranslationId)
            .IsRequired();

        builder.Property(x => x.TranslatedSentences)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne<Example>()
            .WithMany()
            .HasForeignKey(x => x.ExampleId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<TranslationModel>()
            .WithMany(x => x.TranslationExamples)
            .HasForeignKey(x => x.TranslationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new
        {
            x.ExampleId,
            x.TranslationId
        }).IsUnique();
    }
}
