using Vocabulary.Core.Entity.Enum;

namespace Vocabulary.Core.Entity;

public class Dictionary : BaseEntity<Dictionary>
{
    public Guid VocabularyId { get; set; }
    public string Name { get; set; } = default!;
    public string DictionaryUrl { get; set; } = default!;
    public LanguagesType Language { get; set; } = LanguagesType.EN;
}
