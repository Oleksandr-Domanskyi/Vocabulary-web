using MediatR;
using Vocabulary.Application.CQRS.Command;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Handlers.Command
{
    public class DeleteVocabularyCommandHandler : IRequestHandler<DeleteVocabularyCommand, bool>
    {
        private readonly IVocabularyRepository _repository;

        public DeleteVocabularyCommandHandler(IVocabularyRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteVocabularyCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
}
