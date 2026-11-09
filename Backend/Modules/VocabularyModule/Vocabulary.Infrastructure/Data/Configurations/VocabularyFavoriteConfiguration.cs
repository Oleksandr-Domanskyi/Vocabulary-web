using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vocabulary.Core.Entity.UserMarks;
using Vocabulary.Core.Entity.Vocabulary;

namespace Vocabulary.Infrastructure.Data.Configurations;

public sealed class VocabularyFavoriteConfiguration : IEntityTypeConfiguration<VocabularyFavorite>
{
    public void Configure(EntityTypeBuilder<VocabularyFavorite> builder)
    {
        builder.ToTable("VocabularyFavorites", "Vocabulary");
        builder.HasKey(x => new { x.UserId, x.VocabularyId });
        builder.HasOne<VocabularyModel>()
            .WithMany()
            .HasForeignKey(x => x.VocabularyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
