using MediatR;
using Vocabulary.Application.CQRS.Query;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Handlers.Query
{
    public class GetAllVocabularyQueryHandler : IRequestHandler<GetAllVocabularyQuery, List<VocabularyItem>>
    {
        private readonly IVocabularyRepository _repository;

        public GetAllVocabularyQueryHandler(IVocabularyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VocabularyItem>> Handle(GetAllVocabularyQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
