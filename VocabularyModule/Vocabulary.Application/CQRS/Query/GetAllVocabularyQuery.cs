using MediatR;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Core.Entity;

namespace Vocabulary.Application.CQRS.Query;

public class GetAllVocabularyQuery : IRequest<List<VocabularyResponse>>;
