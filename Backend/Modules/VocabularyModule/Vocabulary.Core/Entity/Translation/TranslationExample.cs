using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vocabulary.Core.Entity.Enum;

namespace Vocabulary.Core.Entity.Translation;

public class TranslationExample : BaseEntity<TranslationExample>
{
    public Guid ExampleId { get; set; } = default!;
    public Guid TranslationId { get; set; } = default!;
    public string TranslatedSentences { get; set; } = default!;
}