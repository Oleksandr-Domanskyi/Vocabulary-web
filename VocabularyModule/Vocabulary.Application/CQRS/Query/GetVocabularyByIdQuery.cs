using MediatR;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Core.Entity;

namespace Vocabulary.Application.CQRS.Query;

public class GetVocabularyByIdQuery(Guid Id) : IRequest<VocabularyResponse?>
{
    public Guid Id { get; set; } = Id;
}
