using MediatR;
using Vocabulary.Application.CQRS.Query;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Handlers.Query
{
    public class GetVocabularyByIdQueryHandler : IRequestHandler<GetVocabularyByIdQuery, VocabularyItem?>
    {
        private readonly IVocabularyRepository _repository;

        public GetVocabularyByIdQueryHandler(IVocabularyRepository repository)
        {
            _repository = repository;
        }

        public async Task<VocabularyItem?> Handle(GetVocabularyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByWordAsync(request.Word);
        }
    }
}
