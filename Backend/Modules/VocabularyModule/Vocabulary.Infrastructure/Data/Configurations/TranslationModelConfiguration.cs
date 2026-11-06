
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vocabulary.Core.Entity.Translation;

namespace Vocabulary.Infrastructure.Data.Configurations;

public class TranslationModelConfiguration : IEntityTypeConfiguration<TranslationModel>
{
    public void Configure(EntityTypeBuilder<TranslationModel> builder)
    {
        builder.ToTable("Translations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VocabularyId)
            .IsRequired();

        builder.Property(x => x.TranslatedWord)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.TranslatedDefinition)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.LanguageCode)
            .HasConversion<string>()
            .HasMaxLength(5)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.VocabularyId,
            x.LanguageCode
        }).IsUnique();
    }
}
