using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Core.Entity
{
    public class Example
    {
        public Guid Id { get; set; }
        public string ExampleText { get; set; } = default!;

        public Guid WordId { get; set; }
    }
}