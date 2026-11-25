using MediatR;
using Vocabulary.Core.DTO.Request.Vocabulary;
using Vocabulary.Core.DTO.Response;

namespace Vocabulary.Application.CQRS.Command.CreateVocabulary;

public class CreateVocabularyCommand(CreateVocabularyRequest request) : IRequest<VocabularyResponse>
{
    public CreateVocabularyRequest Vocabulary { get; set; } = request;
}
