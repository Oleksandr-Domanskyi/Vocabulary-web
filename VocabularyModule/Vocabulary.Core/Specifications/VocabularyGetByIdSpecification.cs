using Ardalis.Specification;
using Vocabulary.Core.Entity;

namespace Vocabulary.Core.Specifications;

public class VocabularyGetByIdOrNameSpecification : Specification<VocabularyModel>
{
    public VocabularyGetByIdOrNameSpecification()
    {
        Query.Include(v => v.Examples)
        .Include(x => x.Dictionaries)
        .Include(x => x.Translations)
        .ThenInclude(x => x.TranslationExamples)
        .AsNoTracking();
    }

}
