using MediatR;
using Vocabulary.Application.Common.Errors;
using Vocabulary.Application.Common.Exeptions;
using Vocabulary.Application.CQRS.Query;
using Vocabulary.Application.Mappers.Vocabulary;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Handlers.Query;

public class GetVocabularyByIdQueryHandler(IVocabularyRepository repository) : IRequestHandler<GetVocabularyByIdQuery, VocabularyResponse?>
{
    private readonly IVocabularyRepository _repository = repository;

    public async Task<VocabularyResponse?> Handle(GetVocabularyByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _repository.GetByIdAsync(request.Id);

        return response is null
            ? throw new VocabularyException(VocabularyErrors.NotFound(request.Id))
            : VocabularyMapper.VocabularyModelToResponse(response);
    }
}
