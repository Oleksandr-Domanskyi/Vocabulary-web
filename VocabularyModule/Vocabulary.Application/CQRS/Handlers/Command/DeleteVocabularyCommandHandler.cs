using MediatR;
using Vocabulary.Application.CQRS.Command;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Handlers.Command;

public class DeleteVocabularyCommandHandler(IVocabularyRepository repository) : IRequestHandler<DeleteVocabularyCommand, bool>
{
    private readonly IVocabularyRepository _repository = repository;

    public Task<bool> Handle(DeleteVocabularyCommand request, CancellationToken cancellationToken)
    {
        return _repository.DeleteAsync(request.Id);
    }
}
