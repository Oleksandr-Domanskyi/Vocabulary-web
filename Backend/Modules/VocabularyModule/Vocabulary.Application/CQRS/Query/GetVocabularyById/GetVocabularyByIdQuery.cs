using MediatR;
using Vocabulary.Core.DTO.Response;

namespace Vocabulary.Application.CQRS.Query.GetVocabularyById;

public class GetVocabularyByIdQuery(Guid Id) : IRequest<VocabularyResponse?>
{
    public Guid Id { get; set; } = Id;
}
