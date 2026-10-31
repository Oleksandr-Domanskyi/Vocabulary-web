using MediatR;
using Vocabulary.Application.CQRS.Command;
using Vocabulary.Application.Mappers.Vocabulary;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Handlers.Command;

public class UpdateVocabularyCommandHandler(IVocabularyRepository repository) : IRequestHandler<UpdateVocabularyCommand, VocabularyResponse>
{
    private readonly IVocabularyRepository _repository = repository;

    public async Task<VocabularyResponse> Handle(UpdateVocabularyCommand request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(request.Id);
        return VocabularyMapper.VocabularyModelToResponse(await _repository.UpdateAsync(VocabularyMapper.UpdateRequestToVocabularyModel(model!, request.Vocabulary)));
    }
}
