using MediatR;
using Vocabulary.Core.Entity;

namespace Vocabulary.Application.CQRS.Query
{
    public class GetVocabularyByIdQuery : IRequest<VocabularyItem?>
    {
        public string Word { get; set; } = default!;

        public GetVocabularyByIdQuery(string word)
        {
            Word = word;
        }
    }
}
