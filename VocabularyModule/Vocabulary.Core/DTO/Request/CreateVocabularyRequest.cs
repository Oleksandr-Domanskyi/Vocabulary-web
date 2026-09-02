using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Core.DTO.Request
{
    public class CreateVocabularyRequest
    {
        public string Word { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Definition { get; set; } = default!;
        public string EnglishLevel { get; set; } = default!;
        public List<string>? ExampleTexts { get; set; }
        public Guid? UserId { get; set; }
    }
}