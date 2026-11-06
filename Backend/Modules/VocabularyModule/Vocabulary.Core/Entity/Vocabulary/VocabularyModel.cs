using Vocabulary.Core.Entity.Enum;
using Vocabulary.Core.Entity.Vocabulary;
using Vocabulary.Core.Entity.Translation;

namespace Vocabulary.Core.Entity.Vocabulary;

public class VocabularyModel : BaseEntity<VocabularyModel>
{
    public Guid? UserId { get; set; }
    public string Word { get; set; } = default!;
    public WordType WordType { get; set; }
    public Level Level { get; set; }
    public List<Example> Examples { get; set; } = [];
    public List<Dictionary> Dictionaries { get; set; } = [];
    public List<TranslationModel> Translations { get; set; } = [];
    public string Definition { get; set; } = default!;
    public string Transcription { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //? Azrue Blob
    public string? ImageUrl { get; set; }
}