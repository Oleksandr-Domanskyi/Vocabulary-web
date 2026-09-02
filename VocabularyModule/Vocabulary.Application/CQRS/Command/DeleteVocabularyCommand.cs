using MediatR;

namespace Vocabulary.Application.CQRS.Command
{
    public class DeleteVocabularyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteVocabularyCommand(Guid id)
        {
            Id = id;
        }
    }
}
