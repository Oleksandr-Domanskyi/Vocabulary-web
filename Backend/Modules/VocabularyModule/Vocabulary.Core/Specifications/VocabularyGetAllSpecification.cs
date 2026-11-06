using Ardalis.Specification;
using Vocabulary.Core.Entity;
using Vocabulary.Core.Entity.Vocabulary;

namespace Vocabulary.Core.Specifications;

public class VocabularyGetAllSpecification : Specification<VocabularyModel>
{

    // ?  Add  Paginations

    public VocabularyGetAllSpecification()
    {
        Query.Include(v => v.Examples)
        .Include(x => x.Dictionaries)
        .Include(x => x.Translations)
        .ThenInclude(x => x.TranslationExamples)
        .AsNoTracking();
    }
}
