using MediatR;
using Vocabulary.Application.CQRS.Command;
using Vocabulary.Application.Mappers.Vocabulary;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Handlers.Command;

public class CreateVocabularyCommandHandler(IVocabularyRepository repository) : IRequestHandler<CreateVocabularyCommand, VocabularyResponse>
{
    private readonly IVocabularyRepository _repository = repository;

    public async Task<VocabularyResponse> Handle(CreateVocabularyCommand request, CancellationToken cancellationToken)
    {
        return VocabularyMapper.VocabularyModelToResponse(await _repository.CreateAsync(request.VocabularyItem));
    }
}
