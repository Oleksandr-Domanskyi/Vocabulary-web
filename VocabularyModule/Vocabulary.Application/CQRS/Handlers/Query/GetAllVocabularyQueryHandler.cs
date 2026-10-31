using MediatR;
using Vocabulary.Application.CQRS.Query;
using Vocabulary.Application.Mappers.Vocabulary;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Handlers.Query;

public class GetAllVocabularyQueryHandler(IVocabularyRepository repository) : IRequestHandler<GetAllVocabularyQuery, List<VocabularyResponse>>
{
    private readonly IVocabularyRepository _repository = repository;

    public async Task<List<VocabularyResponse>> Handle(GetAllVocabularyQuery request, CancellationToken cancellationToken)
    {
        return VocabularyMapper.VocabularyModelToResponse(await _repository.GetAllAsync());
    }
}
