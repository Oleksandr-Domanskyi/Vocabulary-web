using FluentValidation;
using Vocabulary.Core.DTO.Request.Transaltion;
using Vocabulary.Core.Entity.Enum;

namespace Vocabulary.Application.Common.Validation;

public sealed class TranslationRequestValidator : AbstractValidator<TranslationRequest>
{
    public TranslationRequestValidator()
    {
        RuleFor(x => x.TranslatedWord).NotEmpty().MaximumLength(500);
        RuleFor(x => x.TranslatedDefinition).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.LanguageCode).EnumName<TranslationRequest, LanguagesType>();

        RuleFor(x => x.TranslationExamples).NotNull();
        RuleForEach(x => x.TranslationExamples)
            .NotNull()
            .SetValidator(new TranslationExampleRequestValidator());
    }
}
