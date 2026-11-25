using Backend.Shared.Validation;
using FluentValidation;
using Vocabulary.Application.Common.Validation;
using Vocabulary.Core.DTO.Request.Transaltion;
using Vocabulary.Core.DTO.Request.Vocabulary;
using Vocabulary.Core.Entity.Enum;

namespace Vocabulary.Application.CQRS.Command.UpdateVocabulary;

public sealed class UpdateVocabularyCommandValidator : AbstractValidator<UpdateVocabularyCommand>
{
    public UpdateVocabularyCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Vocabulary)
            .NotNull()
            .ChildRules(word =>
            {
                word.RuleFor(x => x.Word).Word();
                word.RuleFor(x => x.WordType).EnumName<UpdateVocabularyRequest, WordType>();
                word.RuleFor(x => x.EnglishLevel).EnumName<UpdateVocabularyRequest, Level>();
                word.RuleFor(x => x.Definition).Definition();
                word.RuleFor(x => x.Transcription).Transcription();
                word.RuleFor(x => x.ImageUrl).ImageUrl();

                word.RuleFor(x => x.Examples).NotNull().UniqueIds(x => x.Id);
                word.RuleForEach(x => x.Examples)
                    .NotNull()
                    .SetValidator(new ExampleRequestValidator());

                word.RuleFor(x => x.Dictionaries).NotNull().UniquePerLanguage().UniqueIds(x => x.Id);
                word.RuleForEach(x => x.Dictionaries)
                    .NotNull()
                    .SetValidator(new DictionaryRequestValidator());

                word.RuleFor(x => x.Translations).NotNull().UniquePerLanguage().UniqueIds(x => x.Id);
                word.RuleForEach(x => x.Translations)
                    .NotNull()
                    .SetValidator(new TranslationRequestValidator())
                    .ChildRules(translation => translation
                        .RuleFor(x => x.TranslationExamples)
                        .UniqueIds(x => x.Id)
                        .UniqueIds(x => x.ExampleId, "exampleId")
                        .Must(HaveExampleIdWhenNew)
                        .WithMessage("A new translation example must specify 'exampleId'."))
                    .Must(ReferToExamplesOfTheWord)
                    .WithMessage("Translation examples must refer to examples of this word.");
            })
            .WithoutPropertyPrefix();
    }

    private static bool HaveExampleIdWhenNew(List<TranslationExampleRequest>? examples) =>
        examples is null ||
        examples.TrueForAll(x => x is null || x.Id is not null || x.ExampleId is not null);

    private static bool ReferToExamplesOfTheWord(UpdateVocabularyRequest request, TranslationRequest? translation)
    {
        if (request.Examples is null || translation?.TranslationExamples is null)
            return true;

        var exampleIds = request.Examples
            .Where(x => x?.Id is not null)
            .Select(x => x.Id)
            .ToHashSet();

        return translation.TranslationExamples.TrueForAll(x =>
            x?.ExampleId is null || exampleIds.Contains(x.ExampleId));
    }
}
