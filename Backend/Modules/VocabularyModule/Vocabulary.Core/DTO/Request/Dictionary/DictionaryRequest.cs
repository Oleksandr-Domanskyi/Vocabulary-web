using Vocabulary.Core.Entity.Enum;
using Vocabulary.Core.Entity.Vocabulary;

namespace Vocabulary.Core.DTO.Request.Dictionary;

public class DictionaryRequest
{
    public Guid? Id { get; set; }
    public Guid? VocabularyId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string DictionaryUrl { get; set; } = default!;
    public string Language { get; set; } = default!;
}
