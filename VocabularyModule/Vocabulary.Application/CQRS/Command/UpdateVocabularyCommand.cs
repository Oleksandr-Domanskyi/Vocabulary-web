using MediatR;
using Vocabulary.Core.DTO.Request;
using Vocabulary.Core.Entity;

namespace Vocabulary.Application.CQRS.Command
{
    public class UpdateVocabularyCommand : IRequest<VocabularyItem>
    {
        public VocabularyItem VocabularyItem { get; set; }

        public UpdateVocabularyCommand(Guid Id, UpdateVocabularyRequest request)
        {
            VocabularyItem = new VocabularyItem
            {
                Id = Id,
                Word = request.Word,
                Type = request.Type,
                Definition = request.Definition,
                EnglishLevel = request.EnglishLevel,
                Examples = request.Examples,
                UserId = request.UserId
            };
        }
    }
}
