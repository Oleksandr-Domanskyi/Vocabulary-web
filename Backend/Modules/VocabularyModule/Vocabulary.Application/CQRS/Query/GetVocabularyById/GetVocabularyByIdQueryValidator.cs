using FluentValidation;

namespace Vocabulary.Application.CQRS.Query.GetVocabularyById;

public sealed class GetVocabularyByIdQueryValidator : AbstractValidator<GetVocabularyByIdQuery>
{
    public GetVocabularyByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
