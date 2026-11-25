using FluentValidation;

namespace Vocabulary.Application.CQRS.Command.DeleteVocabulary;

public sealed class DeleteVocabularyCommandValidator : AbstractValidator<DeleteVocabularyCommand>
{
    public DeleteVocabularyCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
