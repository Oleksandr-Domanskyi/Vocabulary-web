
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VocabularyDictionary = Vocabulary.Core.Entity.Dictionary;

namespace Vocabulary.Infrastructure.Data.Configurations;

public class DictionaryConfiguration : IEntityTypeConfiguration<VocabularyDictionary>
{
    public void Configure(EntityTypeBuilder<VocabularyDictionary> builder)
    {
        builder.ToTable("Dictionaries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VocabularyId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.DictionaryUrl)
            .IsRequired()
            .HasMaxLength(2048);

        builder.Property(x => x.Language)
            .HasConversion<string>()
            .HasMaxLength(5)
            .IsRequired();

        builder.HasIndex(x => new { x.VocabularyId, x.Name, x.Language }).IsUnique();
    }
}
