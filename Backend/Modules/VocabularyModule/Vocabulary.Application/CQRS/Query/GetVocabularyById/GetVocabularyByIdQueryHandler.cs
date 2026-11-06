using Backend.Shared.Errors;
using MediatR;
using Vocabulary.Application.Common.Exeptions;
using Vocabulary.Application.Mappers.Vocabulary;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Core.Entity.Vocabulary;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Query.GetVocabularyById;

public class GetVocabularyByIdQueryHandler(IVocabularyRepository repository) : IRequestHandler<GetVocabularyByIdQuery, VocabularyResponse?>
{
    private readonly IVocabularyRepository _repository = repository;

    public async Task<VocabularyResponse?> Handle(GetVocabularyByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _repository.GetByIdAsync(request.Id);

        return response is null
            ? throw new VocabularyException(CommonErrors.NotFound(nameof(VocabularyModel), request.Id))
            : VocabularyMapper.VocabularyModelToResponse(response);
    }
}
