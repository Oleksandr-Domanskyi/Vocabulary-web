using MediatR;
using Vocabulary.Core.DTO.Request;
using Vocabulary.Core.Entity;

namespace Vocabulary.Application.CQRS.Command
{
    public class CreateVocabularyCommand : IRequest<VocabularyItem>
    {
        public VocabularyItem VocabularyItem { get; set; }

        public CreateVocabularyCommand(CreateVocabularyRequest request)
        {
            VocabularyItem = new VocabularyItem
            {
                Id = Guid.NewGuid(),
                Word = request.Word,
                Type = request.Type,
                Definition = request.Definition,
                EnglishLevel = request.EnglishLevel,
                Examples = request.ExampleTexts?.Select(text => new Example { ExampleText = text }).ToList(),
                UserId = request.UserId
            };
        }
    }
}
