using MediatR;
using Vocabulary.Core.DTO.Request.Vocabulary;
using Vocabulary.Core.DTO.Response;

namespace Vocabulary.Application.CQRS.Command.UpdateVocabulary;

public class UpdateVocabularyCommand(Guid Id, UpdateVocabularyRequest request) : IRequest<VocabularyResponse>
{
    public Guid Id { get; set; } = Id;
    public UpdateVocabularyRequest Vocabulary { get; set; } = request;
}
