using MediatR;
using Vocabulary.Application.CQRS.Command;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Handlers.Command
{
    public class CreateVocabularyCommandHandler : IRequestHandler<CreateVocabularyCommand, VocabularyItem>
    {
        private readonly IVocabularyRepository _repository;

        public CreateVocabularyCommandHandler(IVocabularyRepository repository)
        {
            _repository = repository;
        }

        public async Task<VocabularyItem> Handle(CreateVocabularyCommand request, CancellationToken cancellationToken)
        {
            return await _repository.CreateAsync(request.VocabularyItem);
        }
    }
}
