using MediatR;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Command.DeleteVocabulary;

public class DeleteVocabularyCommandHandler(IVocabularyRepository repository) : IRequestHandler<DeleteVocabularyCommand, bool>
{
    private readonly IVocabularyRepository _repository = repository;

    public Task<bool> Handle(DeleteVocabularyCommand request, CancellationToken cancellationToken)
    {
        return _repository.DeleteAsync(request.Id);
    }
}
