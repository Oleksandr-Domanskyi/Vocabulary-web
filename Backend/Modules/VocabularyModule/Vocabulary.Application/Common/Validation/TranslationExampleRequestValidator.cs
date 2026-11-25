using FluentValidation;
using Vocabulary.Core.DTO.Request.Transaltion;

namespace Vocabulary.Application.Common.Validation;

public sealed class TranslationExampleRequestValidator : AbstractValidator<TranslationExampleRequest>
{
    public TranslationExampleRequestValidator()
    {
        RuleFor(x => x.TranslatedSentence).NotEmpty().MaximumLength(1000);
    }
}
