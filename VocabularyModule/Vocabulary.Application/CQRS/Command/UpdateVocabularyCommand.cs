using MediatR;
using Vocabulary.Application.Mappers.Vocabulary;
using Vocabulary.Core.DTO.Request;
using Vocabulary.Core.DTO.Request.Vocabulary;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Core.Entity;

namespace Vocabulary.Application.CQRS.Command;

public class UpdateVocabularyCommand(Guid Id, UpdateVocabularyRequest request) : IRequest<VocabularyResponse>
{
    public Guid Id { get; set; } = Id;
    public UpdateVocabularyRequest Vocabulary { get; set; } = request;
}
