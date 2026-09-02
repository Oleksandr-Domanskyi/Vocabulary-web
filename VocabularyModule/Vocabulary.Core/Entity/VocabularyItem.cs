using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Core.Entity
{
    public class VocabularyItem
    {
        public Guid Id { get; set; }
        public string Word { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Definition { get; set; } = default!;
        public string EnglishLevel { get; set; } = default!;
        public List<Example>? Examples { get; set; }

        public Guid? UserId { get; set; }
    }
}