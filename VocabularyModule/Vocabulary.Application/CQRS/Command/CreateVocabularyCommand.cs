using MediatR;
using Vocabulary.Application.Mappers.Vocabulary;
using Vocabulary.Core.DTO.Request;
using Vocabulary.Core.DTO.Request.Vocabulary;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Core.Entity;

namespace Vocabulary.Application.CQRS.Command;

public class CreateVocabularyCommand(CreateVocabularyRequest request) : IRequest<VocabularyResponse>
{
    public VocabularyModel VocabularyItem { get; set; } = VocabularyMapper.CreateRequestToVocabularyModel(request);
}
