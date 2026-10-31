
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vocabulary.Core.Entity;

namespace Vocabulary.Infrastructure.Data.Configurations;

public class VocabularyModelConfiguration : IEntityTypeConfiguration<VocabularyModel>
{
    public void Configure(EntityTypeBuilder<VocabularyModel> builder)
    {
        builder.ToTable("Vocabulary");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired(false);

        builder.Property(x => x.Word)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.WordType)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Level)
            .HasConversion<string>()
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(x => x.Definition)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Transcription)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(2048)
            .IsRequired(false);

        builder.HasMany(x => x.Examples)
            .WithOne()
            .HasForeignKey(x => x.WordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Dictionaries)
            .WithOne()
            .HasForeignKey(x => x.VocabularyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Translations)
            .WithOne()
            .HasForeignKey(x => x.VocabularyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Word);
        builder.HasIndex(x => x.Level);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.Word });
    }
}
