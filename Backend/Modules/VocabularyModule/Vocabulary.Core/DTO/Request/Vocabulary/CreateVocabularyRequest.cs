
using Vocabulary.Core.DTO.Request.Dictionary;
using Vocabulary.Core.DTO.Request.Example;
using Vocabulary.Core.DTO.Request.Transaltion;
using Vocabulary.Core.Entity.Vocabulary;

namespace Vocabulary.Core.DTO.Request.Vocabulary;

public class CreateVocabularyRequest
{
    public Guid? UserId { get; set; }
    public string Word { get; set; } = default!;
    public string WordType { get; set; } = default!;
    public string EnglishLevel { get; set; } = default!;
    public string Definition { get; set; } = default!;
    public string Transcription { get; set; } = default!;
    public List<ExampleRequest> Examples { get; set; } = [];
    public List<DictionaryRequest> Dictionaries { get; set; } = [];
    public List<TranslationRequest> Translations { get; set; } = [];
    public string? ImageUrl { get; set; }
}
