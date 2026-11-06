using MediatR;

namespace Vocabulary.Application.CQRS.Command.DeleteVocabulary;

public class DeleteVocabularyCommand(Guid id) : IRequest<bool>
{
    public Guid Id { get; set; } = id;
}
