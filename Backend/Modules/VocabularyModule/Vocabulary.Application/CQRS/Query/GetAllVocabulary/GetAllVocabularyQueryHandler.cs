using MediatR;
using Vocabulary.Application.Mappers.Vocabulary;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Query.GetAllVocabulary;

public class GetAllVocabularyQueryHandler(IVocabularyRepository repository) : IRequestHandler<GetAllVocabularyQuery, List<VocabularyResponse>>
{
    private readonly IVocabularyRepository _repository = repository;

    public async Task<List<VocabularyResponse>> Handle(GetAllVocabularyQuery request, CancellationToken cancellationToken)
    {
        return VocabularyMapper.VocabularyModelToResponse(await _repository.GetAllAsync());
    }
}
