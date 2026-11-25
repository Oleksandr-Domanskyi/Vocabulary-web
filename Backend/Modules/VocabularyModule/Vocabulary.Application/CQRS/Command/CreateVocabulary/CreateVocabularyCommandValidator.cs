using Backend.Shared.Validation;
using FluentValidation;
using Vocabulary.Application.Common.Validation;
using Vocabulary.Core.DTO.Request.Vocabulary;
using Vocabulary.Core.Entity.Enum;

namespace Vocabulary.Application.CQRS.Command.CreateVocabulary;

public sealed class CreateVocabularyCommandValidator : AbstractValidator<CreateVocabularyCommand>
{
    public CreateVocabularyCommandValidator()
    {
        RuleFor(x => x.Vocabulary)
            .NotNull()
            .ChildRules(word =>
            {
                word.RuleFor(x => x.UserId).NotEqual(Guid.Empty);
                word.RuleFor(x => x.Word).Word();
                word.RuleFor(x => x.WordType).EnumName<CreateVocabularyRequest, WordType>();
                word.RuleFor(x => x.EnglishLevel).EnumName<CreateVocabularyRequest, Level>();
                word.RuleFor(x => x.Definition).Definition();
                word.RuleFor(x => x.Transcription).Transcription();
                word.RuleFor(x => x.ImageUrl).ImageUrl();

                word.RuleFor(x => x.Examples).NotNull();
                word.RuleForEach(x => x.Examples)
                    .NotNull()
                    .SetValidator(new ExampleRequestValidator());

                word.RuleFor(x => x.Dictionaries).NotNull().UniquePerLanguage();
                word.RuleForEach(x => x.Dictionaries)
                    .NotNull()
                    .SetValidator(new DictionaryRequestValidator());

                word.RuleFor(x => x.Translations).NotNull().UniquePerLanguage();
                word.RuleForEach(x => x.Translations)
                    .NotNull()
                    .SetValidator(new TranslationRequestValidator())
                    // Translation examples are linked to the original examples by index.
                    .Must((request, translation) =>
                        translation?.TranslationExamples is null ||
                        request.Examples is null ||
                        translation.TranslationExamples.Count == request.Examples.Count)
                    .WithMessage("A translation must contain exactly one translation example per example.");
            })
            .WithoutPropertyPrefix();
    }
}
