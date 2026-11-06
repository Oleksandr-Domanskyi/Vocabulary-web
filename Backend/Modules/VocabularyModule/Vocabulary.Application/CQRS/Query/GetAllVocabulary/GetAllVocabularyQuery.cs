using MediatR;
using Vocabulary.Core.DTO.Response;

namespace Vocabulary.Application.CQRS.Query.GetAllVocabulary;

public class GetAllVocabularyQuery : IRequest<List<VocabularyResponse>>;
