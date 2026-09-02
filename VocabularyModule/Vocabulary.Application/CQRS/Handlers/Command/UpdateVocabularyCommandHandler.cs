using MediatR;
using Vocabulary.Application.CQRS.Command;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Application.CQRS.Handlers.Command
{
    public class UpdateVocabularyCommandHandler : IRequestHandler<UpdateVocabularyCommand, VocabularyItem>
    {
        private readonly IVocabularyRepository _repository;

        public UpdateVocabularyCommandHandler(IVocabularyRepository repository)
        {
            _repository = repository;
        }

        public async Task<VocabularyItem> Handle(UpdateVocabularyCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateAsync(request.VocabularyItem);
        }
    }
}
