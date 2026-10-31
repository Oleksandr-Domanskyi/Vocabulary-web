using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vocabulary.Core.Entity.Enum;

namespace Vocabulary.Core.Entity;

public class TranslationModel : BaseEntity<TranslationModel>
{
    public Guid VocabularyId { get; set; }
    public string TranslatedWord { get; set; } = default!;
    public string TranslatedDefinition { get; set; } = default!;
    public LanguagesType LanguageCode { get; set; } = default!;
    public List<TranslationExample> TranslationExamples { get; set; } = [];
}